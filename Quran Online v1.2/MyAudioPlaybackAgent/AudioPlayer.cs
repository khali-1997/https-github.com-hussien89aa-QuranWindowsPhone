using System;
using System.Diagnostics;
using System.Windows;
using Microsoft.Phone.BackgroundAudio;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.IO;

namespace MyAudioPlaybackAgent
{
    public class AudioPlayer : AudioPlayerAgent
    {
        private static volatile bool _classInitialized;
        // A playlist made up of AudioTrack items.
        static List<AudioTrack> _playList = new List<AudioTrack>
        {
             

            // A remote URI
            new AudioTrack(new Uri("http://server11.mp3quran.net/shatri/018.mp3", UriKind.Absolute), 
                            "Episode 29", 
                            "Windows Phone Radio", 
                            "Windows Phone Radio Podcast",
                            null )
        };
        // What's the current track?
        static int currentTrackNumber = 0;

        void loadfile()
        {
            string filePath = "authorAYA.txt";
            using (var store = IsolatedStorageFile.GetUserStoreForApplication())
            {

                try
                {
                   
                    
                    string contents;
                    using (StreamReader reader = new StreamReader(store.OpenFile(filePath, FileMode.Open, FileAccess.Read)))
                    {    _playList.Clear();
                        contents = reader.ReadLine();
                        while (!(contents == null))
                        {
                            string[] OneAya = contents.Split('*');

                             if (OneAya[4].Equals("Absolute"))
                            {
                                _playList.Add(
                                      new AudioTrack(new Uri(OneAya[0], UriKind.Absolute),
                            OneAya[1],
                            OneAya[2],
                            OneAya[3],
                              null));
                                currentTrackNumber = Convert.ToInt32(OneAya[5]);
                            }
                             else
                             {
                                 _playList.Add(
                                       new AudioTrack(new Uri(OneAya[0], UriKind.RelativeOrAbsolute),
                             OneAya[1],
                             OneAya[2],
                             OneAya[3],
                            null));
                                 currentTrackNumber = Convert.ToInt32(OneAya[5]);
                             }
                            contents = reader.ReadLine();
                        }
                        reader.Close(); 
                    }
                }
                catch (IsolatedStorageException ex)
                {


                }


            }
        }
       

        /// <remarks>
        /// AudioPlayer instances can share the same process. 
        /// Static fields can be used to share state between AudioPlayer instances
        /// or to communicate with the Audio Streaming agent.
        /// </remarks>
        public AudioPlayer()
        {
            if (!_classInitialized)
            {
                _classInitialized = true;
                // Subscribe to the managed exception handler
                Deployment.Current.Dispatcher.BeginInvoke(delegate
                {
                    Application.Current.UnhandledException += AudioPlayer_UnhandledException;
                });
            }
        }

        /// Code to execute on Unhandled Exceptions
        private void AudioPlayer_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            if (System.Diagnostics.Debugger.IsAttached)
            {
                // An unhandled exception has occurred; break into the debugger
              //  System.Diagnostics.Debugger.Break();
            }
        }


        /// <summary>
        /// Increments the currentTrackNumber and plays the correpsonding track.
        /// </summary>
        /// <param name="player">The BackgroundAudioPlayer</param>
        private void PlayNextTrack(BackgroundAudioPlayer player)
        {
            if (++currentTrackNumber >= _playList.Count)
            {
                currentTrackNumber = 0;
            }

            PlayTrack(player);
        }


        /// <summary>
        /// Decrements the currentTrackNumber and plays the correpsonding track.
        /// </summary>
        /// <param name="player">The BackgroundAudioPlayer</param>
        private void PlayPreviousTrack(BackgroundAudioPlayer player)
        {
            if (--currentTrackNumber < 0)
            {
                currentTrackNumber = _playList.Count - 1;
            }

            PlayTrack(player);
        }


        /// <summary>
        /// Plays the track in our playlist at the currentTrackNumber position.
        /// </summary>
        /// <param name="player">The BackgroundAudioPlayer</param>
        /// 
        private void PlayTrack(BackgroundAudioPlayer player)
        {
            if (PlayState.Paused == player.PlayerState)
            {
                // If we're paused, we already have 
                // the track set, so just resume playing.
                player.Play();
            }
            else
            {
                 // What's the current track?
        
                // Set which track to play. When the TrackReady state is received 
                // in the OnPlayStateChanged handler, call player.Play().
                // loadfile();
                player.Track = _playList[currentTrackNumber];
            }

        }


        /// <summary>
        /// Called when the playstate changes, except for the Error state (see OnError)
        /// </summary>
        /// <param name="player">The BackgroundAudioPlayer</param>
        /// <param name="track">The track playing at the time the playstate changed</param>
        /// <param name="playState">The new playstate of the player</param>
        /// <remarks>
        /// Play State changes cannot be cancelled. They are raised even if the application
        /// caused the state change itself, assuming the application has opted-in to the callback.
        /// 
        /// Notable playstate events: 
        /// (a) TrackEnded: invoked when the player has no current track. The agent can set the next track.
        /// (b) TrackReady: an audio track has been set and it is now ready for playack.
        /// 
        /// Call NotifyComplete() only once, after the agent request has been completed, including async callbacks.
        /// </remarks>
        protected override void OnPlayStateChanged(BackgroundAudioPlayer player, AudioTrack track, PlayState playState)
        {
        
            switch (playState)
            {
                case PlayState.TrackEnded:
                  //  PlayNextTrack(player);
                    break;

                case PlayState.TrackReady:
                   
                    // The track to play is set in the PlayTrack method.
                    player.Play();
                    break;
            }

            NotifyComplete();
        }


        /// <summary>
        /// Called when the user requests an action using application/system provided UI
        /// </summary>
        /// <param name="player">The BackgroundAudioPlayer</param>
        /// <param name="track">The track playing at the time of the user action</param>
        /// <param name="action">The action the user has requested</param>
        /// <param name="param">The data associated with the requested action.
        /// In the current version this parameter is only for use with the Seek action,
        /// to indicate the requested position of an audio track</param>
        /// <remarks>
        /// User actions do not automatically make any changes in system state; the agent is responsible
        /// for carrying out the user actions if they are supported.
        /// 
        /// Call NotifyComplete() only once, after the agent request has been completed, including async callbacks.
        /// </remarks>
        protected override void OnUserAction(BackgroundAudioPlayer player, AudioTrack track, UserAction action, object param)
        {
            switch (action)
            {
                case UserAction.Play:
                    loadfile();
                    PlayTrack(player);
                    break;

                case UserAction.Pause:
                    player.Pause();
                    break;
                case UserAction.Stop:
                    player.Stop();
                    break;
                case UserAction.SkipPrevious:
                    PlayPreviousTrack(player);
                    break;

                case UserAction.SkipNext:
                    PlayNextTrack(player);
                    break;
                case UserAction.Seek:
                    
                    player.Position = (TimeSpan)param;
                    break;
            }

            NotifyComplete();
        }


        /// <summary>
        /// Called whenever there is an error with playback, such as an AudioTrack not downloading correctly
        /// </summary>
        /// <param name="player">The BackgroundAudioPlayer</param>
        /// <param name="track">The track that had the error</param>
        /// <param name="error">The error that occured</param>
        /// <param name="isFatal">If true, playback cannot continue and playback of the track will stop</param>
        /// <remarks>
        /// This method is not guaranteed to be called in all cases. For example, if the background agent 
        /// itself has an unhandled exception, it won't get called back to handle its own errors.
        /// </remarks>
        protected override void OnError(BackgroundAudioPlayer player, AudioTrack track, Exception error, bool isFatal)
        {
            if (isFatal)
            {
                Abort();
            }
            else
            {
                NotifyComplete();
            }

        }

        /// <summary>
        /// Called when the agent request is getting cancelled
        /// </summary>
        /// <remarks>
        /// Once the request is Cancelled, the agent gets 5 seconds to finish its work,
        /// by calling NotifyComplete()/Abort().
        /// </remarks>
        protected override void OnCancel()
        {

        }
    }
}