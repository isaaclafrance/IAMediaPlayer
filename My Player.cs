using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Collections.Specialized;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using System.Timers;
using Alvas.Audio;
using Player.Properties;
using Yeti.MMedia;
using Yeti.MMedia.Mp3;
using gma.System.Windows;
using WaveLib;

namespace Player
{
    public partial class My_Player : Form
    {
        #region Variable Field
            Random rand;
            System.Timers.Timer player_timer;
            System.Timers.Timer recorder_timer;
            System.Timers.Timer recording_second_tick;
            System.Timers.Timer timer_second_tick;
            System.Timers.Timer timer_play_all;
            DateTime recorder_time;
            DateTime timer_time;
            StringDictionary recorderSources;

            bool firstTimeRecorded;
            bool playAll;
            int playAllMode;
            int W_M_Total_Plays;
            int Web_Total_Plays;
            ToolTip infobubbleTooltip;
            ToolTip itp;
            Edit_Main_Button_Dialog edit_main_a;
            Edit_Main_Button_Dialog edit_main_b;
            Edit_Main_Button_Dialog edit_main_c;
            Converting_Progress c_progress;
            Player player;
            Find_Web_Player_Dialog find_web_player_dialog;
            Scheduler scheduler;
            StringDictionary W_M_Playlist_Dictionary;
            StringDictionary Web_Playlist_Dictionary;
            public StringDictionary Main_Button_Playlist_Dictionary;
            ProgressBar convert_progress;
            ProgressBar saving_progress;
            Label convert_label;
            Label saving_label;
            string previously_converted;
            string scheduleSaveDirectory;
            string full_W_M_NowPlaying_Name;
            string full_Web_NowPlaying_Name;
            int current_W_M_track;
            string trackAddress;
            List<string> W_M_keyValueItems;
            int total_W_M_tracks;
            string saveDirectory;
            public string full_MainButton1_Name;
            public string full_MainButton2_Name;
            public string full_MainButton3_Name;
            public string scheduledFileName;
            public bool isScheduling;
            
            delegate void Stop(string type);
            delegate void Start(string name, string address, string type);
            event Stop stop_player;
            event Start start_player;

            UserActivityHook inputHook;
            public string initial_rec_location;
            public bool is_convertingFormat_CompressedWav;
            public bool is_convertingFormat_MP3;
            public bool is_recording_paused;
            public bool is_recording_player_paused;
            public string newConvertingFormat_fileExtension;
            public IntPtr newConvertingFormat;
            public RecorderEx recorder;
            public Alvas.Audio.WaveWriter recordingWriter;
            public Stream _recordingStream;
            Alvas.Audio.Player recording_player;
            delegate void CheckFormatState(string format);
            delegate void SetState(object state);
            delegate void SetCallback(string text);
            delegate void SetControl();
        #endregion

        public My_Player()
        {
            InitializeComponent();
            rand = new Random();
            firstTimeRecorded = true;
            playAll = false;
            player_timer = new System.Timers.Timer();
            timer_play_all = new System.Timers.Timer(3000);
            player = new Player(this);
            Web_Playlist_Dictionary = new StringDictionary();
            W_M_Playlist_Dictionary = new StringDictionary();
            W_M_keyValueItems = new List<string>();
            Main_Button_Playlist_Dictionary = new StringDictionary();
            inputHook = new UserActivityHook();
            infobubbleTooltip = new ToolTip();
            Web_Total_Plays = 0;
            W_M_Total_Plays = 0;
            itp = new ToolTip();

            timer_second_tick = new System.Timers.Timer(1000);
            timer_time = new DateTime();

            initial_rec_location = Path.ChangeExtension(Application.ExecutablePath, ".wav");
            newConvertingFormat_fileExtension = "mp3";
            recorder = new RecorderEx();
            recorder.Open += new System.EventHandler(recorder_Open);
            recorder.Data += new RecorderEx.DataEventHandler(recorder_Data);
            recorder.Close += new System.EventHandler(recorder_Close);
            _recordingStream = null;
            recording_player = new Alvas.Audio.Player();
            recorder_timer = new System.Timers.Timer();
            recording_second_tick = new System.Timers.Timer(1000);
            recorder_time = new DateTime();
            scheduler= new Scheduler(this);
            isScheduling = false;

            #region Create Save Diretory

            string pre_directory = Application.StartupPath; 
            scheduleSaveDirectory = @""+pre_directory + "\\Scheduled_Recordings";

            System.IO.Directory.CreateDirectory(scheduleSaveDirectory);

            #endregion
        }

        private void My_Player_Load(object sender, EventArgs e)
        {
            #region My Player
                Hook_Keyboard_ToolTip.SetToolTip(Hook_Keyboard_CheckBox, "Home key: Hides Control\n"+"End key: Shows Control\n" + "Page Down key: Stop Player\n");
                inputHook.KeyDown += new KeyEventHandler(inputHook_KeyDown);
                inputHook.Start();

                //Setup info tooltip object
                infobubbleTooltip.ToolTipTitle = "Important Info";
                infobubbleTooltip.ToolTipIcon = ToolTipIcon.Info;
                infobubbleTooltip.IsBalloon = true;
                infobubbleTooltip.UseFading = true;
                infobubbleTooltip.UseAnimation = true;
                infobubbleTooltip.AutomaticDelay = 50;
                infobubbleTooltip.AutoPopDelay = 20000;
                infobubbleTooltip.InitialDelay = 15;
                infobubbleTooltip.ReshowDelay = 10;

                Main_Button_1.Text = "";
                Main_Button_2.Text = "";
                Main_Button_3.Text = "";

                full_MainButton1_Name = "";
                full_MainButton2_Name = "";
                full_MainButton3_Name = "";

                timer_second_tick.Elapsed += new ElapsedEventHandler(timer_second_tick_Elapsed);
                timer_play_all.Elapsed += new ElapsedEventHandler(timer_play_all_Elapsed);

                player_timer.Elapsed += new ElapsedEventHandler(Force_Stop_Player);
                player_timer.Elapsed += new ElapsedEventHandler(Make_Visible);
                stop_player += new Stop(Stop_Player);
                start_player += new Start(Start_Player);

                player.Activate();
            #endregion

            #region Player
                player.WindowsMediaPlayer.ErrorEvent += new EventHandler(WindowsMediaPlayer_ErrorEvent);
            #endregion

            #region ComboBoxes
                try
                {
                    total_W_M_tracks = Settings.Default.W_M_Names.Count;
                    if ( Settings.Default.Web_Names.Count > 0 ||  Settings.Default.W_M_Names.Count > 0)
                    {
                        #region Load Playlist into Comboboxes
                        foreach (string item in Settings.Default.W_M_Names)
                        {
                            Windows_Media_Name_ComboBox.Items.Add(item);
                            Windows_Media_Name_ComboBox.Sorted = true;
                            Windows_Media_Name_ComboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
                            Windows_Media_Name_ComboBox.AutoCompleteMode = AutoCompleteMode.Suggest;
                        }
                        foreach (string item in Settings.Default.Web_Names)
                        {
                            Web_Name_ComboBox.Items.Add(item);
                            Web_Name_ComboBox.Sorted = true;
                            Web_Name_ComboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
                            Web_Name_ComboBox.AutoCompleteMode = AutoCompleteMode.Suggest;
                        }
                        #endregion

                        #region Organize Playlist into Dictionaries
                        for (int i = 0; i < Settings.Default.W_M_Names.Count; i++)
                        {
                            W_M_Playlist_Dictionary.Add(Settings.Default.W_M_Names[i], Settings.Default.W_M_Locations[i]);
                        }
                        for (int i = 0; i < Settings.Default.Web_Names.Count; i++)
                        {
                            Web_Playlist_Dictionary.Add(Settings.Default.Web_Names[i], Settings.Default.Web_Locations[i]);
                        }
                        #endregion

                        #region
                            Windows_Media_Name_ComboBox.Items.Add("__Play All Once In Order"); Windows_Media_Name_ComboBox.Items.Add("__Play All Repeat In Order");
                            Windows_Media_Name_ComboBox.Items.Add("__Play All Once Rand"); Windows_Media_Name_ComboBox.Items.Add("__Play All Repeat Rand");
                            W_M_keyValueItems.Clear();
                            foreach (string key in W_M_Playlist_Dictionary.Values)
                            {
                                W_M_keyValueItems.Add(key);
                            }
                            if (Web_Playlist_ComboBox.Items.Count != Web_Name_ComboBox.Items.Count)
                            {
                                Web_Playlist_ComboBox.Items.Clear();

                                foreach (string item in Web_Playlist_Dictionary.Keys)
                                {
                                    Web_Playlist_ComboBox.Items.Add(item);
                                }
                            }
                            if (Windows_Media_Playlist_ComboBox.Items.Count != Windows_Media_Name_ComboBox.Items.Count)
                            {
                                Windows_Media_Playlist_ComboBox.Items.Clear();

                                foreach (string item in W_M_Playlist_Dictionary.Keys)
                                {
                                    Windows_Media_Playlist_ComboBox.Items.Add(item);
                                }
                            }
                        #endregion
                    }
                    else
                    {
                        Settings.Default.W_M_Locations = new StringCollection();
                        Settings.Default.W_M_Names = new StringCollection();
                        Settings.Default.Web_Locations = new StringCollection();
                        Settings.Default.Web_Names = new StringCollection();
                        #region
                        Windows_Media_Name_ComboBox.Items.Add("__Play All Once In Order"); Windows_Media_Name_ComboBox.Items.Add("__Play All Repeat In Order");
                        Windows_Media_Name_ComboBox.Items.Add("__Play All Once Rand"); Windows_Media_Name_ComboBox.Items.Add("__Play All Repeat Rand");
                        #endregion
                    }
                }
                catch 
                {
                    Settings.Default.W_M_Locations = new StringCollection();
                    Settings.Default.W_M_Names = new StringCollection();
                    Settings.Default.Web_Locations = new StringCollection();
                    Settings.Default.Web_Names = new StringCollection();
                }
            #endregion

            #region Main Buttons
                try
                {
                    if (Settings.Default.Main_Button_Names.Count > 0)
                    {
                        #region Name Main Buttons

                        if (Settings.Default.Main_Button_Names.Count >= 1) 
                        {
                            full_MainButton1_Name = Settings.Default.Main_Button_Names[0].ToUpper();

                            if (full_MainButton1_Name.Length >= 8)
                            {
                                Main_Button_1.Text = new string(full_MainButton1_Name.ToCharArray(0, 7));
                                Main_Button_1.Text += " ...";
                            }
                            else
                            {
                                Main_Button_1.Text = full_MainButton1_Name;
                            }
                        }
                        if (Settings.Default.Main_Button_Names.Count >= 2)
                        {
                            full_MainButton2_Name = Settings.Default.Main_Button_Names[1].ToUpper();

                            if (full_MainButton2_Name.Length >= 8)
                            {
                                Main_Button_2.Text = new string(full_MainButton2_Name.ToCharArray(0, 7));
                                Main_Button_2.Text += " ...";
                            }
                            else
                            {
                                Main_Button_2.Text = full_MainButton2_Name;
                            }
                        }

                        if (Settings.Default.Main_Button_Names.Count == 3)
                        {
                            full_MainButton3_Name = Settings.Default.Main_Button_Names[2].ToUpper();

                            if (full_MainButton3_Name.Length >= 8)
                            {
                                Main_Button_3.Text = new string(full_MainButton3_Name.ToCharArray(0, 7));
                                Main_Button_3.Text += " ...";
                            }
                            else
                            {
                                Main_Button_3.Text = full_MainButton3_Name;
                            }
                        }
                        #endregion

                        #region Organize Main Button Playlist into Dictionaries
                        for (int i = 0; i < Settings.Default.Main_Button_Names.Count; i++)
                        {
                            Main_Button_Playlist_Dictionary.Add(Settings.Default.Main_Button_Names[i], Settings.Default.Main_Button_Locations[i]);
                        }
                        #endregion
                    }
                    else
                    {
                        Settings.Default.Main_Button_Names = new StringCollection();
                        Settings.Default.Main_Button_Locations = new StringCollection();
                    }
                }
                catch
                {
                    Settings.Default.Main_Button_Names = new StringCollection();
                    Settings.Default.Main_Button_Locations = new StringCollection();
                }
            #endregion
             
            #region Set Defaults For Main Button Buttons
                if (Settings.Default.Main_Button_Names.Count == 0)
                {
                    if (!Main_Button_Playlist_Dictionary.ContainsKey("PANDORA"))
                    {
                        full_MainButton1_Name = "Pandora";
                        Main_Button_1.Text = "PANDORA";
                        Main_Button_Playlist_Dictionary.Add("PANDORA", "www.pandora.com" + "&web");
                    }

                    if (!Main_Button_Playlist_Dictionary.ContainsKey("Bach Concerto in D minor"))
                    {
                        full_MainButton2_Name = "Bach Concerto in D Minor";
                        Main_Button_2.Text = "BACH CONCERTO";
                        Main_Button_Playlist_Dictionary.Add("Bach Concerto in D Minor", "www.youtube.com/watch?v=8-KyL2gMxV8" + "&web");
                    }
                }
           #endregion
            #region Set Defaults For Playlists
                if (Settings.Default.Web_Names.Count == 0 && Settings.Default.Web_Locations.Count == 0)
                {
                    #region Add Item 1
                    Web_Name_ComboBox.Items.Add("bach - fugue bwv 578");
                    Web_Playlist_Dictionary.Add("fugue - bwv 578", "www.youtube.com/watch?v=pVadl4ocX0M");
                    #endregion

                    #region Add Item 2
                    Web_Name_ComboBox.Items.Add("piazzolla - libertango");
                    Web_Playlist_Dictionary.Add("piazzolla - libertango", "www.youtube.com/watch?v=RUAPf_ccobc");
                    #endregion

                    #region Add Item 3
                    Web_Name_ComboBox.Items.Add("bach - english suite no.1 bwv 806 bouree");
                    Web_Playlist_Dictionary.Add("bach english suite no.1 bwv 806 bouree", "www.youtube.com/watch?v=aPUCl9C5U4I");
                    #endregion

                    #region Add Item 4
                    Web_Name_ComboBox.Items.Add("vivaldi - summer presto");
                    Web_Playlist_Dictionary.Add("vivaldi - summer presto", "www.youtube.com/watch?v=g65oWFMSoK0");
                    #endregion

                    #region Finalize Additions
                    if (Web_Playlist_ComboBox.Items.Count != Web_Name_ComboBox.Items.Count)
                    {
                        Web_Playlist_ComboBox.Items.Clear();

                        foreach (string item in Web_Playlist_Dictionary.Keys)
                        {
                            Web_Playlist_ComboBox.Items.Add(item);
                        }
                    }
                    #endregion
                }
            #endregion

                #region Recorder
                recorder_timer.Elapsed += new ElapsedEventHandler(recorder_timer_Elapsed);
                recording_second_tick.Elapsed += new ElapsedEventHandler(recording_second_tick_Elapsed);
            #endregion
        }

        #region My Player Control
            void timer_second_tick_Elapsed(object sender, ElapsedEventArgs e)
            {
                string displayed_time;

                timer_time = timer_time.Subtract(new TimeSpan(0, 0, 1));
                displayed_time = Convert.ToString(timer_time.Minute + timer_time.Hour * 60) + "." + Convert.ToString(timer_time.Second/60.0).Replace("0.", "");

                Set_Timer_Minute_Time_Textbox(displayed_time);
            }
            void Set_Timer_Minute_Time_Textbox(string text)
            {
                if (this.Set_Time_Textbox.InvokeRequired)
                {
                    SetCallback d = new SetCallback(Set_Timer_Minute_Time_Textbox);
                    this.Invoke(d, new object[] { text });
                }
                else
                {
                    this.Set_Time_Textbox.Text = text;
                }
            }

            private void Start_Timer_Button_Click(object sender, EventArgs e)
            {
                try
                {
                    if (Set_Time_Textbox.Text != "")
                    {
                        int hour = 0;
                        int minute = 0;
                        double pre_minute = Double.Parse(Set_Time_Textbox.Text);

                        if (pre_minute == 0.0 || pre_minute == 0)
                        {
                            minute = 1;
                            Set_Time_Textbox.Text = "1";
                        }
                        if (pre_minute < 1)
                        {
                            minute = 1;
                            Set_Time_Textbox.Text = "1";
                        }
                        if (pre_minute < 60)
                        {
                            minute = Int32.Parse(Set_Time_Textbox.Text);
                        }
                        if (pre_minute >= 60)
                        {
                            double x = Double.Parse(Set_Time_Textbox.Text) / 60;

                            hour = Int32.Parse(Set_Time_Textbox.Text) / 60;
                            double y = Double.Parse(x.ToString()) - hour;

                            minute = Int32.Parse(y.ToString()) * 60;
                        }

                        player_timer.Interval = (hour * 60 + minute) * 60000;
                        player_timer.Start();
                        timer_time = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, hour, minute, 0);
                        timer_second_tick.Start();

                        Timer_State_CheckBox.Checked = true;
                        Set_Time_Textbox.Enabled = false;
                    }
                    else
                    {
                        Schedule_Recording_toolTip.Show("Please Type in a Time", Set_Time_Textbox);
                        Schedule_Recording_toolTip.Show("Please Type in a Time", Set_Time_Textbox);
                    }
                }
                catch
                {
                    Set_Time_Textbox.Text = "Only #'s";
                }
            }
            private void Stop_Timer_Button_Click(object sender, EventArgs e)
            {
                player_timer.Stop();
                timer_second_tick.Stop();
                timer_play_all.Stop();

                Timer_State_CheckBox.Checked = false;
                Set_Time_Textbox.Enabled = true;
            }

            void Force_Stop_Player(object sender, EventArgs e)
            {
                try
                {
                    stop_player("web");
                    stop_player("windows media");

                    player_timer.Stop();
                    timer_second_tick.Stop();

                    Set_Time_Textbox.Enabled = true;
                }
                catch
                {
                    try
                    {
                        Application.SetSuspendState(PowerState.Suspend, true, false);
                    }
                    catch
                    {
                        //Process.Start("ShutDown", "/s");
                    }
                }
            }
            void Start_Player(string name, string address, string type)
            {
                if (type == "web")
                {
                    UriBuilder URL = new UriBuilder(address);
                    player.address.Url = URL.Uri;

                    string displayed_name = "";

                    if (name.Length >= 12)
                    {
                        full_Web_NowPlaying_Name = name;
                        displayed_name = new string(name.ToCharArray(0, 12));
                        displayed_name += "...";
                    }
                    else
                    {
                        displayed_name = name;
                    }

                    Web_NowPlaying_Label.Text = displayed_name;
                    Web_Total_Plays++;
                }

                if (type == "windows media")
                {
                    string displayed_name = "";
                    try
                    {
                        switch (name)
                        {
                            case "__Play All Once In Order":
                                playAll = true;
                                playAllMode = 0;
                                current_W_M_track = 0;
                                trackAddress = W_M_keyValueItems[current_W_M_track];
                                player.WindowsMediaPlayer.URL = trackAddress;

                                if (name.Length >= 12)
                                {
                                    full_W_M_NowPlaying_Name = name;
                                    displayed_name = new string(name.ToCharArray(0, 12));
                                    displayed_name += "...";
                                }
                                else
                                {
                                    displayed_name = name;
                                }

                                W_M_NowPlaying_Label.Text = displayed_name;

                                #region
                                if (!Player_Visibility.Checked)
                                {
                                    infobubbleTooltip.Show("CLICK 'Show Player' Checkbox to view the playing media", Player_Visibility);
                                    infobubbleTooltip.Show("CLICK 'Show Player' Checkbox to view the playing media", Player_Visibility);
                                }
                                W_M_Total_Plays = 1;
                                #endregion
                                timer_play_all.Start();
                                break;
                            case "__Play All Repeat In Order":
                                playAll = true;
                                playAllMode = 1;
                                current_W_M_track = 0;
                                trackAddress = W_M_keyValueItems[current_W_M_track];
                                player.WindowsMediaPlayer.URL = trackAddress;

                                if (name.Length >= 12)
                                {
                                    full_W_M_NowPlaying_Name = name;
                                    displayed_name = new string(name.ToCharArray(0, 12));
                                    displayed_name += "...";
                                }
                                else
                                {
                                    displayed_name = name;
                                }

                                W_M_NowPlaying_Label.Text = displayed_name;

                                #region
                                if (!Player_Visibility.Checked)
                                {
                                    infobubbleTooltip.Show("CLICK 'Show Player' Checkbox to view the playing media", Player_Visibility);
                                    infobubbleTooltip.Show("CLICK 'Show Player' Checkbox to view the playing media", Player_Visibility);
                                }
                                W_M_Total_Plays = 1;
                                #endregion
                                timer_play_all.Start();
                                break;
                            case "__Play All Once Rand":
                                playAll = true;
                                playAllMode = 2;
                                current_W_M_track = rand.Next(total_W_M_tracks);
                                trackAddress = W_M_keyValueItems[current_W_M_track];
                                player.WindowsMediaPlayer.URL = trackAddress;

                                if (name.Length >= 12)
                                {
                                    full_W_M_NowPlaying_Name = name;
                                    displayed_name = new string(name.ToCharArray(0, 12));
                                    displayed_name += "...";
                                }
                                else
                                {
                                    displayed_name = name;
                                }

                                W_M_NowPlaying_Label.Text = displayed_name;

                                #region
                                if (!Player_Visibility.Checked)
                                {
                                    infobubbleTooltip.Show("CLICK 'Show Player' Checkbox to view the playing media", Player_Visibility);
                                    infobubbleTooltip.Show("CLICK 'Show Player' Checkbox to view the playing media", Player_Visibility);
                                }
                                W_M_Total_Plays = 1;
                                #endregion
                                timer_play_all.Start();
                                break;
                            case "__Play All Repeat Rand":
                                playAll = true;
                                playAllMode = 3;
                                current_W_M_track = rand.Next(total_W_M_tracks);
                                trackAddress = W_M_keyValueItems[current_W_M_track];
                                player.WindowsMediaPlayer.URL = trackAddress;

                                if (name.Length >= 12)
                                {
                                    full_W_M_NowPlaying_Name = name;
                                    displayed_name = new string(name.ToCharArray(0, 12));
                                    displayed_name += "...";
                                }
                                else
                                {
                                    displayed_name = name;
                                }

                                W_M_NowPlaying_Label.Text = displayed_name;

                                #region
                                if (!Player_Visibility.Checked)
                                {
                                    infobubbleTooltip.Show("CLICK 'Show Player' Checkbox to view the playing media", Player_Visibility);
                                    infobubbleTooltip.Show("CLICK 'Show Player' Checkbox to view the playing media", Player_Visibility);
                                }
                                W_M_Total_Plays = 1;
                                #endregion
                                timer_play_all.Start();
                                break;
                            default:
                                playAll = false;
                                player.WindowsMediaPlayer.URL = address;

                                if (name.Length >= 12)
                                {
                                    full_W_M_NowPlaying_Name = name;
                                    displayed_name = new string(name.ToCharArray(0, 12));
                                    displayed_name += "...";
                                }
                                else
                                {
                                    displayed_name = name;
                                }

                                W_M_NowPlaying_Label.Text = displayed_name;

                                #region
                                if (!Player_Visibility.Checked)
                                {
                                    infobubbleTooltip.Show("CLICK 'Show Player' Checkbox to view the playing media", Player_Visibility);
                                    infobubbleTooltip.Show("CLICK 'Show Player' Checkbox to view the playing media", Player_Visibility);
                                }
                                W_M_Total_Plays++;
                                #endregion
                                timer_play_all.Start();
                                break;
                        }
                    }
                    catch
                    {
                        itp.IsBalloon = true;
                        itp.Show("There are no playable items in this playlist. Goto to the 'Playlist Edit' tab and try adding some.", Windows_Media_Name_ComboBox);
                        itp.Show("There are no playable items in this playlist. Goto to the 'Playlist Edit' tab and try adding some.", Windows_Media_Name_ComboBox);
                    }
                }
            }
            void Stop_Player(string type)
            {
                if (type == "web")
                {
                    UriBuilder Url = new UriBuilder("about:blank");
                    player.address.Url = Url.Uri;

                    Web_NowPlaying_Label.Text = "Nothing";
                }

                if (type == "windows media")
                {
                    playAll = false;

                    player.WindowsMediaPlayer.Ctlcontrols.stop();

                    W_M_NowPlaying_Label.Text = "Nothing";
                }
            }
            void Make_Visible(object sender, EventArgs e)
            {
                this.Show();
            }
            void Make_Invisible_Button_MouseHover(object sender, EventArgs e)
            {
                toolTip.ToolTipIcon = ToolTipIcon.Info;
                toolTip.ToolTipTitle = "Info";
                toolTip.Show("Press the 'Home' key to make visible again", Make_Invisible);
            }
            void inputHook_KeyDown(object sender, KeyEventArgs e)
            {
                switch (e.KeyData.ToString())
                {
                    case "Home":
                        if (!this.Visible) { this.Show(); }
                        break;

                    case "End":
                        if (this.Visible) { this.Hide(); }
                        break;

                    case "Next":
                        stop_player("web");
                        stop_player("windows media");
                        break;

                    default:
                        break;
                }
            }
            void WindowsMediaPlayer_ErrorEvent(object sender, EventArgs e)
            {
                W_M_Location_Error_ToolTip.Show("This media's location:\n" + "  1. Is incorrect\n" + "  2. Does not exist anymore\n" + "Remove this media from the playlist and add it again", Windows_Media_Name_ComboBox);
                W_M_Location_Error_ToolTip.Show("This media's location:\n" + "  1. Is incorrect\n" + "  2. Does not exist anymore\n" + "Remove this media from the playlist and add it again", Windows_Media_Name_ComboBox);
            }
            void timer_play_all_Elapsed(object sender, ElapsedEventArgs e)
            {
                if (player.WindowsMediaPlayer.playState == WMPLib.WMPPlayState.wmppsStopped)
                {
                    if (playAll == true)
                    {
                        switch (playAllMode)
                        {
                            #region
                            case 0:
                                current_W_M_track++;
                                if (W_M_Total_Plays == total_W_M_tracks)
                                {
                                    playAll = false;
                                    W_M_NowPlaying_Label.Text = "Nothing";

                                    itp.IsBalloon = true;
                                    itp.Show("All  playlist items have been played.", Windows_Media_Name_ComboBox);
                                    itp.Show("All  playlist items have been played.", Windows_Media_Name_ComboBox);

                                    timer_play_all.Stop();
                                    break;
                                }
                                trackAddress = W_M_keyValueItems[current_W_M_track];
                                player.WindowsMediaPlayer.URL = trackAddress;
                                W_M_Total_Plays++;
                                //MessageBox.Show(trackAddress);
                                break;
                            case 1:
                                current_W_M_track++;
                                if (W_M_Total_Plays == total_W_M_tracks)
                                {
                                    current_W_M_track = 0;
                                    W_M_Total_Plays = 0;
                                }

                                trackAddress = W_M_keyValueItems[current_W_M_track];
                                player.WindowsMediaPlayer.URL = trackAddress;
                                W_M_Total_Plays++;

                                break;
                            case 2:
                                current_W_M_track = rand.Next(total_W_M_tracks);
                                if (W_M_Total_Plays == total_W_M_tracks)
                                {
                                    playAll = false;
                                    W_M_NowPlaying_Label.Text = "Nothing";

                                    itp.IsBalloon = true;
                                    itp.Show("All Windows Media playlist items have been played.", Windows_Media_Name_ComboBox);
                                    itp.Show("All Windows Media playlist items have been played.", Windows_Media_Name_ComboBox);

                                    timer_play_all.Stop();
                                    break;
                                }

                                trackAddress = W_M_keyValueItems[current_W_M_track];
                                player.WindowsMediaPlayer.URL = trackAddress;
                                W_M_Total_Plays++;

                                break;
                            case 3:
                                current_W_M_track = rand.Next(total_W_M_tracks);
                                if (W_M_Total_Plays == total_W_M_tracks)
                                {
                                    W_M_Total_Plays = 0;
                                }

                                trackAddress = W_M_keyValueItems[current_W_M_track];
                                player.WindowsMediaPlayer.URL = trackAddress;
                                W_M_Total_Plays++;

                                break;
                            default:
                                break;
                            #endregion
                        }
                    }
                    if (playAll == false)
                    {
                        W_M_NowPlaying_Label.Text = "Nothing";
                    }
                }                
            }

            private void Main_Button_1_Click(object sender, MouseEventArgs e)
            {
                if (full_MainButton1_Name == "" || e.Button == MouseButtons.Right)
                {
                    if (true)//(edit_main_a == null)
                    {
                        edit_main_a = new Edit_Main_Button_Dialog(this, 1);
                        edit_main_a.Show();
                    }
                }
                else
                {
                    if (full_MainButton1_Name != "" && e.Button == MouseButtons.Left && Main_Button_Playlist_Dictionary[full_MainButton1_Name].Contains("&web"))
                    {
                        start_player(full_MainButton1_Name, Main_Button_Playlist_Dictionary[full_MainButton1_Name].Replace("&web", ""), "web");
                        Web_Player_RadioButton.Checked = true;
                    }
                    if (full_MainButton1_Name != "" && e.Button == MouseButtons.Left && Main_Button_Playlist_Dictionary[full_MainButton1_Name].Contains("&windows media"))
                    {
                        start_player(full_MainButton1_Name, Main_Button_Playlist_Dictionary[full_MainButton1_Name].Replace("&windows media", ""), "windows media");
                        Windows_Media_Player_RadioButton.Checked = true;
                    }
                    if (!Player_Visibility.Checked)
                    {
                        infobubbleTooltip.Show("CLICK 'Show Player' Checkbox to view the playing media", Player_Visibility);
                        infobubbleTooltip.Show("CLICK 'Show Player' Checkbox to view the playing media", Player_Visibility);
                    }
                }
            }
            private void Main_Button_2_Click(object sender, MouseEventArgs e)
            {
                if (full_MainButton2_Name == "" || e.Button == MouseButtons.Right)
                {
                    if (true)//(edit_main_b == null)
                    {
                        edit_main_b = new Edit_Main_Button_Dialog(this, 2);
                        edit_main_b.Show();
                    }
                }
                else
                {
                    if (full_MainButton2_Name != "" && e.Button == MouseButtons.Left && Main_Button_Playlist_Dictionary[full_MainButton2_Name].Contains("&web"))
                    {
                        start_player(full_MainButton2_Name, Main_Button_Playlist_Dictionary[full_MainButton2_Name].Replace("&web", ""), "web");
                    }
                    if (full_MainButton2_Name != "" && e.Button == MouseButtons.Left && Main_Button_Playlist_Dictionary[full_MainButton2_Name].Contains("&windows media"))
                    {
                        start_player(full_MainButton2_Name, Main_Button_Playlist_Dictionary[full_MainButton2_Name].Replace("&windows media", ""), "windows media");
                    }
                    if (!Player_Visibility.Checked)
                    {
                        infobubbleTooltip.Show("CLICK 'Show Player' Checkbox to view the playing media", Player_Visibility);
                        infobubbleTooltip.Show("CLICK 'Show Player' Checkbox to view the playing media", Player_Visibility);
                    }
                }
            }
            private void Main_Button_3_Click(object sender, MouseEventArgs e)
            {
                if (full_MainButton3_Name == "" || e.Button == MouseButtons.Right)
                {
                    if (true)//(edit_main_c == null)
                    {
                        edit_main_c = new Edit_Main_Button_Dialog(this, 3);
                        edit_main_c.Show();
                    }
                }
                else
                {
                    if (full_MainButton3_Name != "" && e.Button == MouseButtons.Left && Main_Button_Playlist_Dictionary[full_MainButton3_Name].Contains("&web"))
                    {
                        start_player(full_MainButton3_Name, Main_Button_Playlist_Dictionary[full_MainButton3_Name].Replace("&web", ""), "web");
                    }
                    if (full_MainButton3_Name != "" && e.Button == MouseButtons.Left && Main_Button_Playlist_Dictionary[full_MainButton3_Name].Contains("&windows media"))
                    {
                        start_player(full_MainButton3_Name, Main_Button_Playlist_Dictionary[full_MainButton3_Name].Replace("&windows media", ""), "windows media");
                    }
                    if (!Player_Visibility.Checked)
                    {
                        infobubbleTooltip.Show("CLICK 'Show Player' Checkbox to view the playing media", Player_Visibility);
                        infobubbleTooltip.Show("CLICK 'Show Player' Checkbox to view the playing media", Player_Visibility);
                    }
                }
            }
            private void Main_Button_1_MouseHover(object sender, EventArgs e)
            {
                if (full_MainButton1_Name == "")
                {
                    toolTip.ToolTipIcon = ToolTipIcon.Info;
                    toolTip.ToolTipTitle = "Help";
                    toolTip.Show("Please Click Here To Asscociate a Media With This Button", Main_Button_1);
                }
                if (full_MainButton1_Name != "")
                {
                    toolTip.ToolTipIcon = ToolTipIcon.None;
                    toolTip.ToolTipTitle = "Info";
                    toolTip.Show(full_MainButton1_Name + "\n\nEdit this button by right clicking it.", Main_Button_1);
                }
            }
            private void Main_Button_2_MouseHover(object sender, EventArgs e)
            {
                if (full_MainButton2_Name == "")
                {
                    toolTip.ToolTipIcon = ToolTipIcon.Info;
                    toolTip.ToolTipTitle = "Help";
                    toolTip.Show("Please Click Here To Asscociate a Media With This Button", Main_Button_2);
                }
                if (full_MainButton2_Name != "")
                {
                    toolTip.ToolTipIcon = ToolTipIcon.None;
                    toolTip.ToolTipTitle = "Info";
                    toolTip.Show(full_MainButton2_Name + "\n\nEdit this button by right clicking it.", Main_Button_2);
                }
            }
            private void Main_Button_3_MouseHover(object sender, EventArgs e)
            {
                if (full_MainButton3_Name == "")
                {
                    toolTip.ToolTipIcon = ToolTipIcon.Info;
                    toolTip.ToolTipTitle = "Help";
                    toolTip.Show("Please Click Here To Asscociate a Media With This Button", Main_Button_3);
                }
                if (full_MainButton3_Name != "")
                {
                    toolTip.ToolTipIcon = ToolTipIcon.None;
                    toolTip.ToolTipTitle = "Info";
                    toolTip.Show(full_MainButton3_Name + "\n\nEdit this button by right clicking it.", Main_Button_3);
                }
            }

            private void Begin_Stream_Click(object sender, EventArgs e)
            {
                if (Web_Player_RadioButton.Checked && Web_Name_ComboBox.SelectedIndex >= 0)
                {
                    string name = (string)Web_Name_ComboBox.SelectedItem;
                    string location = Web_Playlist_Dictionary[(string)Web_Name_ComboBox.SelectedItem];

                    start_player(name, location, "web");
                    Web_Name_ComboBox.Text = "";
                }

                if (Windows_Media_Player_RadioButton.Checked && Windows_Media_Name_ComboBox.SelectedIndex >= 0)
                {
                    string name = (string)Windows_Media_Name_ComboBox.SelectedItem;
                    string location = W_M_Playlist_Dictionary[(string)Windows_Media_Name_ComboBox.SelectedItem];

                    start_player(name, location, "windows media");
                    Windows_Media_Name_ComboBox.Text = "";
                }
            }
            private void End_Stream_Click(object sender, EventArgs e)
            {
                if (Web_Player_RadioButton.Checked)
                {
                    stop_player("web");
                }

                if (Windows_Media_Player_RadioButton.Checked)
                {
                    stop_player("windows media");
                }
            }

            private void Web_NowPlaying_Label_MouseMove(object sender, MouseEventArgs e)
            {
                Web_NowPlaying_Label.ForeColor = Color.Blue;
            }
            private void W_M_NowPlaying_Label_MouseMove(object sender, MouseEventArgs e)
            {
                W_M_NowPlaying_Label.ForeColor = Color.Blue;
            }
            private void Web_NowPlaying_Label_MouseLeave(object sender, EventArgs e)
            {
                Web_NowPlaying_Label.ForeColor = Color.Black;
            }
            private void W_M_NowPlaying_Label_MouseLeave(object sender, EventArgs e)
            {
                W_M_NowPlaying_Label.ForeColor = Color.Black;
            }

            private void Web_NowPlaying_Label_MouseHover(object sender, EventArgs e)
            {
                if (Web_NowPlaying_Label.Text.Contains("..."))
                {
                    toolTip.ToolTipIcon = ToolTipIcon.Info;
                    toolTip.ToolTipTitle = "Info";
                    toolTip.Show(full_Web_NowPlaying_Name, Web_NowPlaying_Label);
                }
            }
            private void W_M_NowPlaying_Label_MouseHover(object sender, EventArgs e)
            {
                if (W_M_NowPlaying_Label.Text.Contains("..."))
                {
                    toolTip.ToolTipIcon = ToolTipIcon.Info;
                    toolTip.ToolTipTitle = "Info";
                    toolTip.Show(full_W_M_NowPlaying_Name, W_M_NowPlaying_Label);
                }
            }

            private void Web_Name_ComboBox_SelectionChangeCommitted(object sender, EventArgs e)
            {
                Web_Player_RadioButton.Checked = true;
            }
            private void Windows_Media_Name_ComboBox_SelectedIndexChanged(object sender, EventArgs e)
            {
                Windows_Media_Player_RadioButton.Checked = true;
            }

            private void Make_Invisible_Click(object sender, EventArgs e)
            {
                this.Hide();
            }
            private void Player_Visibility_CheckedChanged(object sender, EventArgs e)
            {
                switch (Player_Visibility.CheckState)
                {
                    case CheckState.Checked:
                        player.Show();
                        break;
                    case CheckState.Indeterminate:
                        player.Opacity = 40;
                        break;
                    case CheckState.Unchecked:
                        player.Hide();
                        break;
                    default:
                        break;
                }
            }

            private void Hook_Keyboard_CheckBox_CheckedChanged(object sender, EventArgs e)
            {
                switch (Hook_Keyboard_CheckBox.CheckState)
                {
                    case CheckState.Checked:
                        inputHook.Start();
                        break;
                    case CheckState.Indeterminate:
                        break;
                    case CheckState.Unchecked:
                        inputHook.Stop();
                        break;
                    default:
                        break;
                }
            }
            private void Set_Time_Textbox_MouseHover(object sender, EventArgs e)
            {
                Set_Timer_Tooltip.SetToolTip(Set_Time_Textbox, "Max Minutes: Infinte\n" + "Min Minutes: 1\n" + "Max Decimal Places: 0");
            }
        #endregion

        #region Playlist Edit
            public void Browse_Button1_Click(object sender, EventArgs e)
            {
                Uri home;
                UriBuilder uri_builder = new UriBuilder("www.google.com");
                home = uri_builder.Uri;

                //openFileDialog1.ShowDialog();
                find_web_player_dialog = new Find_Web_Player_Dialog(this);
                find_web_player_dialog.Show();
                find_web_player_dialog.webBrowser1.Url = home;
            }
            public void Browse_Button2_Click(object sender, EventArgs e)
            {
                openFileDialog2.ShowDialog();
            }

            private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
            {
                Web_Location_TextBox.Text = openFileDialog1.FileName;
            }
            private void openFileDialog2_FileOk(object sender, CancelEventArgs e)
            {
                Windows_Media_Location_TextBox.Text = openFileDialog2.FileName;
            }

            private void Add_Web_Player_Playlist_Button_Click(object sender, EventArgs e)
            {
                if ((Web_Name_TextBox.Text != "" || Web_Location_TextBox.Text != "") && !Web_Playlist_Dictionary.ContainsKey(Web_Name_TextBox.Text.ToLower()) && !Web_Playlist_Dictionary.ContainsValue(Web_Location_TextBox.Text))
                {
                    Web_Name_ComboBox.Items.Add(Web_Name_TextBox.Text.ToLower());

                    Web_Playlist_Dictionary.Add(Web_Name_TextBox.Text.ToLower(), Web_Location_TextBox.Text);

                    Web_Name_TextBox.Text = "";
                    Web_Location_TextBox.Text = "";

                    if (Web_Playlist_ComboBox.Items.Count != Web_Name_ComboBox.Items.Count)
                    {
                        Web_Playlist_ComboBox.Items.Clear();

                        foreach (string item in Web_Playlist_Dictionary.Keys)
                        {
                            Web_Playlist_ComboBox.Items.Add(item);
                        }
                    }
                }

                if (Web_Playlist_Dictionary.ContainsKey(Web_Name_TextBox.Text.ToLower()))
                {
                    Invalid_Name_ToolTip.Show("Name already exists.\n" + "Type another name.", Web_Name_TextBox);
                    Invalid_Name_ToolTip.Show("Name already exists.\n" + "Type another name.", Web_Name_TextBox);
                }
                if (Web_Playlist_Dictionary.ContainsValue(Web_Location_TextBox.Text))
                {
                    Invalid_Location_ToolTip.Show("Location already exists.\n" + "Choose another name.", Web_Location_TextBox);
                    Invalid_Location_ToolTip.Show("Location already exists.\n" + "Choose another name.", Web_Location_TextBox);
                }
            }
            private void Add_Media_Player_Playlist_Button_Click(object sender, EventArgs e)
            {
                if ((Windows_Media_Name_Textbox.Text != "" || Windows_Media_Location_TextBox.Text != "") && !W_M_Playlist_Dictionary.ContainsKey(Windows_Media_Name_Textbox.Text.ToLower()) && !W_M_Playlist_Dictionary.ContainsValue(Windows_Media_Location_TextBox.Text.ToLower()))
                {
                    Windows_Media_Name_ComboBox.Items.Add(Windows_Media_Name_Textbox.Text.ToLower());

                    W_M_Playlist_Dictionary.Add(Windows_Media_Name_Textbox.Text.ToLower(), Windows_Media_Location_TextBox.Text.ToLower());

                    Windows_Media_Name_Textbox.Text = "";
                    Windows_Media_Location_TextBox.Text = "";

                    W_M_keyValueItems.Clear();
                    foreach (string key in W_M_Playlist_Dictionary.Values)
                    {
                        W_M_keyValueItems.Add(key);
                    }

                    if (Windows_Media_Playlist_ComboBox.Items.Count != Windows_Media_Name_ComboBox.Items.Count)
                    {
                        Windows_Media_Playlist_ComboBox.Items.Clear();

                        foreach (string item in W_M_Playlist_Dictionary.Keys)
                        {
                            Windows_Media_Playlist_ComboBox.Items.Add(item);
                        }
                    }

                    total_W_M_tracks = W_M_Playlist_Dictionary.Count;

                }

                if (W_M_Playlist_Dictionary.ContainsKey(Windows_Media_Name_Textbox.Text.ToLower()))
                {
                    Invalid_Name_ToolTip.Show("Name already exists.\n" + "Type another name.", Windows_Media_Name_Textbox);
                    Invalid_Name_ToolTip.Show("Name already exists.\n" + "Type another name.", Windows_Media_Name_Textbox);
                }
                if (W_M_Playlist_Dictionary.ContainsValue(Windows_Media_Location_TextBox.Text.ToLower()))
                {
                    Invalid_Location_ToolTip.Show("Location already exists.\n" + "Choose another name.", Windows_Media_Location_TextBox);
                    Invalid_Location_ToolTip.Show("Location already exists.\n" + "Choose another name.", Windows_Media_Location_TextBox);
                }
            }

            private void Web_Player_RadioButton_CheckedChanged(object sender, EventArgs e)
            {
                if (Web_Player_RadioButton.Checked)
                {
                    Windows_Media_Player_RadioButton.Checked = false;
                    player.Player_tabControl.SelectTab(0);
                }
            }
            private void Windows_Media_Player_RadioButton_CheckedChanged(object sender, EventArgs e)
            {
                if (Windows_Media_Player_RadioButton.Checked)
                {
                    Web_Player_RadioButton.Checked = false;
                    player.Player_tabControl.SelectTab(1);
                }
            }

            private void Web_Playlist_ComboBox_MouseMove(object sender, MouseEventArgs e)
            {
                if (Web_Playlist_ComboBox.Items.Count != Web_Name_ComboBox.Items.Count)
                {
                    Web_Playlist_ComboBox.Items.Clear();

                    foreach (string item in Web_Playlist_Dictionary.Keys)
                    {
                        Web_Playlist_ComboBox.Items.Add(item);
                    }
                }
            }
            private void Windows_Media_Playlist_ComboBox_MouseMove(object sender, MouseEventArgs e)
            {
                if (Windows_Media_Playlist_ComboBox.Items.Count != Windows_Media_Name_ComboBox.Items.Count)
                {
                    Windows_Media_Playlist_ComboBox.Items.Clear();

                    foreach (string item in W_M_Playlist_Dictionary.Keys)
                    {
                        Windows_Media_Playlist_ComboBox.Items.Add(item);
                    }
                }
            }

            private void Remove_Web_Playlist_Button_Click(object sender, EventArgs e)
            {
                string item = (string)Web_Playlist_ComboBox.SelectedItem;

                Web_Name_ComboBox.Items.Remove(item);
                Web_Playlist_ComboBox.Items.Remove(item);
                Web_Playlist_Dictionary.Remove(item);
                Web_Playlist_ComboBox.Text = "";

                if (Web_NowPlaying_Label.Text == item)
                {
                    stop_player("web");
                }

                if (Web_Playlist_ComboBox.Items.Count != Web_Name_ComboBox.Items.Count)
                {
                    Web_Playlist_ComboBox.Items.Clear();

                    foreach (string item2 in Web_Playlist_Dictionary.Keys)
                    {
                        Web_Playlist_ComboBox.Items.Add(item2);
                    }
                }
            }
            private void Remove_Windows_Media_Playlist_Button_Click(object sender, EventArgs e)
            {
                string item = (string)Windows_Media_Playlist_ComboBox.SelectedItem;

                Windows_Media_Name_ComboBox.Items.Remove(item);
                Windows_Media_Playlist_ComboBox.Items.Remove(item);
                W_M_Playlist_Dictionary.Remove(item);
                Windows_Media_Playlist_ComboBox.Text = "";

                if (W_M_NowPlaying_Label.Text == item)
                {
                    stop_player("windows media");
                }
                W_M_keyValueItems.Clear();
                foreach (string key in W_M_Playlist_Dictionary.Values)
                {
                    W_M_keyValueItems.Add(key);
                }

                if (Windows_Media_Playlist_ComboBox.Items.Count != Windows_Media_Name_ComboBox.Items.Count)
                {
                    Windows_Media_Playlist_ComboBox.Items.Clear();

                    foreach (string item2 in W_M_Playlist_Dictionary.Keys)
                    {
                        Windows_Media_Playlist_ComboBox.Items.Add(item2);
                    }
                }

                total_W_M_tracks = W_M_Playlist_Dictionary.Count;
            } 
        #endregion

        #region Sound Recorder
            void recorder_timer_Elapsed(object sender, ElapsedEventArgs e)
            {
                this.stop_recorder();
            }
            void recording_second_tick_Elapsed(object sender, ElapsedEventArgs e)
            {
                recorder_time = recorder_time.Subtract(new TimeSpan(0, 0, 1));

                this.Set_Recording_Hour_Time_Textbox(Convert.ToString(recorder_time.Hour));
                this.Set_Recording_Minute_Time_Textbox(Convert.ToString(recorder_time.Minute));
                this.Set_Recording_Second_Time_Textbox(Convert.ToString(recorder_time.Second));
            }

            private void Recording_Hour_TextBox_Leave(object sender, EventArgs e)
            {
                if (Recording_Hour_TextBox.Text == "")
                {
                    Recording_Hour_TextBox.Text = "Hour";
                }
            }
            private void Recording_Minute_TextBox_Click(object sender, EventArgs e)
            {
                Recording_Minute_TextBox.Text = "";
            }
            private void Recording_Second_TextBox_Click(object sender, EventArgs e)
            {
                Recording_Second_TextBox.Text = "";
            }
            private void Recording_Hour_TextBox_Click(object sender, EventArgs e)
            {
                Recording_Hour_TextBox.Text = "";
            }
            private void Recording_Minute_TextBox_Leave(object sender, EventArgs e)
            {
                if (Recording_Minute_TextBox.Text == "")
                {
                    Recording_Minute_TextBox.Text = "Min"; 
                }
            }
            private void Recording_Second_TextBox_Leave(object sender, EventArgs e)
            {
                if (Recording_Second_TextBox.Text == "")
                {
                    Recording_Second_TextBox.Text = "Sec"; 
                }
            }
            public void Set_Recording_Hour_Time_Textbox(string text)
            {
                if (this.Recording_Minute_TextBox.InvokeRequired)
                {
                    SetCallback d = new SetCallback(Set_Recording_Hour_Time_Textbox);
                    this.Invoke(d, new object[] { text });
                }
                else
                {
                    this.Recording_Hour_TextBox.Text = text;
                }
            }
            public void Set_Recording_Minute_Time_Textbox(string text)
            {
                if (this.Recording_Minute_TextBox.InvokeRequired)
                {
                    SetCallback d = new SetCallback(Set_Recording_Minute_Time_Textbox);
                    this.Invoke(d, new object[] { text });
                }
                else
                {
                    this.Recording_Minute_TextBox.Text = text;
                }
            }
            public void Set_Recording_Second_Time_Textbox(string text)
            {
                if (this.Recording_Second_TextBox.InvokeRequired)
                {
                    SetCallback d = new SetCallback(Set_Recording_Second_Time_Textbox);
                    this.Invoke(d, new object[] { text });
                }
                else
                {
                    this.Recording_Second_TextBox.Text = text;
                }
            }

            void recorder_Open(object sender, EventArgs e)
            {
                if (firstTimeRecorded == true)
                {
                    firstTimeRecorded = false;
                }
                if (recordingWriter != null)
                {
                    recordingWriter.Close();
                }
                recordingWriter = new Alvas.Audio.WaveWriter(_recordingStream, recorder.FormatBytes());
            }            
            void recorder_Data(object sender, DataEventArgs e)
            {
                byte[] data = e.Data;
                recordingWriter.WriteData(data);
            }
            void recorder_Close(object sender, EventArgs e)
            {
                _recordingStream.Close();
                recordingWriter.Close();
                recordingWriter = null;
            }
            public void stop_recorder()
            {
                if (this.Stop_Recording_Button.InvokeRequired)
                {
                    SetControl stop = new SetControl(stop_recorder);
                    this.Invoke(stop);
                }
                else
                {
                    this.Stop_Recording_Button.PerformClick();
                }
            }

            public void Start_Recording_Button_Click(object sender, EventArgs e)
            {
                if (Recording_CheckBox.Checked)
                {
                    switch (is_recording_paused)
                    {
                        case true:
                            recorder.StartRecord();
                            Start_Recording_Button.Text = "Pause";
                            is_recording_paused = false;
                            break;
                        case false:
                            recorder.PauseRecord();
                            Start_Recording_Button.Text = "Resume";
                            is_recording_paused = true;
                            break;
                        default:
                            recorder.PauseRecord();
                            Start_Recording_Button.Text = "Resume";
                            is_recording_paused = true;
                            break;
                    }  
                }
                else
                {
                    //Create a list of recordable sources
                    recorderSources = new StringDictionary();
                    for (int i = 0; i < RecorderEx.RecorderCount; i++)
                    {
                        recorderSources.Add(RecorderEx.GetRecorderName(i), i.ToString());
                        //MessageBox.Show(RecorderEx.GetRecorderName(i));
                    }
                    try
                    {
                        Delete_Recorded_Button.PerformClick();

                        string fileName = initial_rec_location;
                        recorder.Format = AudioCompressionManager.GetPcmFormat(2, 16, 48000);
                        _recordingStream = new FileStream(fileName, FileMode.Create);

                        if (recorderSources.Count > 1)
                        {
                            int num = -1;
                            if (recorderSources.ContainsKey("Stereo Mix")) { num = Int32.Parse(recorderSources["Stereo Mix"]); }
                            if (recorderSources.ContainsKey("stereo mix")) { num = Int32.Parse(recorderSources["stereo mix"]); }
                            if (recorderSources.ContainsKey("StereoMix")) { num = Int32.Parse(recorderSources["StereoMix"]); }
                            if (recorderSources.ContainsKey("stereomix")) { num = Int32.Parse(recorderSources["stereomix"]); }
                            if (recorderSources.ContainsKey("Mono Mix")) { num = Int32.Parse(recorderSources["Mono Mix"]); }
                            if (recorderSources.ContainsKey("mono mix")) { num = Int32.Parse(recorderSources["mono mix"]); }
                            if (recorderSources.ContainsKey("MonoMix")) { num = Int32.Parse(recorderSources["MonoMix"]); }
                            if (recorderSources.ContainsKey("monomix")) { num = Int32.Parse(recorderSources["monomix"]); }
                            if (recorderSources.ContainsKey("Wave Out Mix")) { num = Int32.Parse(recorderSources["Wave Out Mix"]); }
                            if (recorderSources.ContainsKey("wave out mix")) { num = Int32.Parse(recorderSources["wave out mix"]); }
                            if (recorderSources.ContainsKey("WaveOutMix")) { num = Int32.Parse(recorderSources["WaveOutMix"]); }
                            if (recorderSources.ContainsKey("waveoutmix")) { num = Int32.Parse(recorderSources["waveoutmix"]); }

                            recorder.RecorderID = num;
                        }
                        else
                        {
                            recorder.RecorderID = 0;
                        }

                        itp.IsBalloon = true;
                        itp.Show("Current Recording Source: "+ RecorderEx.GetRecorderName(recorder.RecorderID), Recording_CheckBox);
                        itp.Show("Current Recording Source: " + RecorderEx.GetRecorderName(recorder.RecorderID), Recording_CheckBox);

                        recorder.StartRecord();
                        Start_Recording_Button.Text = "Pause";
                    }
                    catch (Exception fail)
                    {
                        MessageBox.Show(fail.Message);
                    }
                }

                if (recorder.State == DeviceState.InProgress)
                {
                    Recording_CheckBox.Checked = true;
                }

                #region Disable Unusable Controls
                    Recording_Hour_TextBox.Enabled = false;
                    Recording_Minute_TextBox.Enabled = false;
                    Recording_Second_TextBox.Enabled = false;
                    Record_with_Timer_Button.Enabled = false;
                    //Start_Recording_Button.Enabled = false;

                    Play_Recorded_Button.Enabled = false;
                    Stop_Recorded_Button.Enabled = false;
                    Delete_Recorded_Button.Enabled = false;

                    Recording_Location_Textbox.Enabled = false;
                    Browse_Recording_Location.Enabled = false;
                    Save_Recording_Button.Enabled = false;
                #endregion

                Start_Recording_GroupBox.ForeColor = Color.Blue;
                Test_Recording_GroupBox.ForeColor = Color.Red;
                if (Save_GroupBox.ForeColor == Color.Red)
                {
                    Save_GroupBox.ForeColor = Color.Blue;
                }
            }
            public void Record_with_Timer_Button_Click(object sender, EventArgs e)
            {
                double hour = 0.0;
                double minutes = 0.0;
                double seconds = 0.0;

                #region
                    if (Recording_Hour_TextBox.Text == "Hour" || Recording_Hour_TextBox.Text == "0")
                    {
                        hour = 0.0;
                    }
                    if (Recording_Minute_TextBox.Text == "Min" || Recording_Minute_TextBox.Text == "0")
                    {
                        minutes = 0.0;
                    }
                    if (Recording_Second_TextBox.Text == "Sec" || Recording_Second_TextBox.Text == "0")
                    {
                        Recording_Second_TextBox.Text = Convert.ToString(10.0);
                    } 
                #endregion

                #region
                if (Recording_Hour_TextBox.Text != "Hour")
                {
                    try
                    {
                        hour = Convert.ToDouble(Recording_Hour_TextBox.Text);
                    }
                    catch
                    {
                        Timed_Recording_Error_ToolTip.Show("The timer received invalid inputs.\nPlease put numbers and not letters or symbols into the timer textboxes.", Recording_Hour_TextBox);
                        Timed_Recording_Error_ToolTip.Show("The timer received invalid inputs.\nPlease put numbers and not letters or symbols into the timer textboxes.", Recording_Hour_TextBox);
                        return;
                    }
                }

                if (Recording_Minute_TextBox.Text != "Min")
                {
                    try
                    {
                        minutes = Convert.ToDouble(Recording_Minute_TextBox.Text);
                    }
                    catch
                    {
                        Timed_Recording_Error_ToolTip.Show("The timer received invalid inputs.\nPlease put numbers and not letters or symbols into the timer textboxes.", Recording_Minute_TextBox);
                        Timed_Recording_Error_ToolTip.Show("The timer received invalid inputs.\nPlease put numbers and not letters or symbols into the timer textboxes.", Recording_Minute_TextBox);
                        return;
                    }
                }

                if (Recording_Second_TextBox.Text != "Sec")
                {
                    try
                    {
                        seconds = Convert.ToDouble(Recording_Second_TextBox.Text);
                    }
                    catch
                    {
                        Timed_Recording_Error_ToolTip.Show("The timer received invalid inputs.\nPlease put numbers and not letters or symbols into the timer textboxes.", Recording_Second_TextBox);
                        Timed_Recording_Error_ToolTip.Show("The timer received invalid inputs.\nPlease put numbers and not letters or symbols into the timer textboxes.", Recording_Second_TextBox);
                        return;
                    }
                }
                #endregion

                #region
                    recorder_time = new DateTime(1, 1, 1, (int)hour, (int)minutes, (int)seconds);

                    recorder_timer.Interval = hour * 60000 * 60 + minutes * 60000 + seconds * 1000;
                    recorder_timer.Start();
                    recording_second_tick.Start();

                    Delete_Recorded_Button_Click(null, null);
                    Start_Recording_Button_Click(null, null);
                #endregion
            }
            public void Stop_Recording_Button_Click(object sender, EventArgs e)
            {
                if (Recording_CheckBox.Checked == true)
                {
                    recorder.StopRecord();

                    recorder_timer.Stop();
                    recording_second_tick.Stop();

                    #region Enable Unusable Controls
                    Recording_Hour_TextBox.Enabled = true;
                    Recording_Minute_TextBox.Enabled = true;
                    Recording_Second_TextBox.Enabled = true;
                    Record_with_Timer_Button.Enabled = true;
                    Start_Recording_Button.Enabled = true;
                    Stop_Recording_Button.Enabled = true;

                    Play_Recorded_Button.Enabled = true;
                    Stop_Recorded_Button.Enabled = true;
                    Delete_Recorded_Button.Enabled = true;

                    Recording_Location_Textbox.Enabled = true;
                    Browse_Recording_Location.Enabled = true;
                    Save_Recording_Button.Enabled = true;
                    #endregion

                    #region Show Schedule Recording

                    #endregion

                    if (recorder.State == DeviceState.Stopped)
                    {
                        Recording_CheckBox.Checked = false;
                        Recording_Hour_TextBox.Text = "Hour";
                        Recording_Minute_TextBox.Text = "Min";
                        Recording_Second_TextBox.Text = "Sec";
                        Start_Recording_Button.Text = "Record";
                    }
                }
            }

            private void Schedule_Recording_checkBox_MouseHover(object sender, EventArgs e)
            {
                Schedule_Recording_toolTip.ToolTipTitle = "Info";
                Schedule_Recording_toolTip.SetToolTip(Schedule_Recording_checkBox, "Click Here to Schedule a Recording for Later");
                Schedule_Recording_toolTip.SetToolTip(Schedule_Recording_checkBox, "Click Here to Schedule a Recording for Later");
            }
            private void Schedule_Recording_checkBox_CheckedChanged(object sender, EventArgs e)
            {
                switch (Schedule_Recording_checkBox.CheckState)
                {
                    case CheckState.Checked:
                        if (firstTimeRecorded == true)
                        {
                            Recording_Second_TextBox.Text = "1";
                            Record_with_Timer_Button.PerformClick();
                        }
                        scheduler.Show();
                        break;
                    case CheckState.Indeterminate:
                        break;
                    case CheckState.Unchecked:
                        scheduler.Hide();
                        if (scheduler.s_c_d1 != null)
                        {
                            scheduler.s_c_d1.Hide(); 
                        }
                        if (scheduler.s_c_d2 != null)
                        {
                            scheduler.s_c_d2.Hide(); 
                        }
                        if (scheduler.s_c_d3 != null)
                        {
                            scheduler.s_c_d3.Hide(); 
                        }
                        if (scheduler.s_c_d4 != null)
                        {
                            scheduler.s_c_d4.Hide();
                        }
                        if (scheduler.s_c_d5 != null)
                        {
                            scheduler.s_c_d5.Hide();
                        }
                        if (scheduler.s_c_d6 != null)
                        {
                            scheduler.s_c_d6.Hide();
                        }
                        break;
                    default:
                        break;
                }
            }

            private void Play_Recorded_Button_Click(object sender, EventArgs e)
            {
                if (Is_Recording_Playing_CheckBox.Checked)
                {
                    switch (is_recording_player_paused)
                    {
                        case true:
                            recording_player.Resume();
                            Play_Recorded_Button.Text = "Pause";
                            is_recording_player_paused = false;
                            break;
                        case false:
                            recording_player.Pause();
                            Play_Recorded_Button.Text = "Resume";
                            is_recording_player_paused = true;
                            break;
                        default:
                            recording_player.Pause();
                            Play_Recorded_Button.Text = "Resume";
                            is_recording_player_paused = true;
                            break;
                    }
                }
                else
                {
                    recording_player.FileName = initial_rec_location;

                    if (recording_player.Play() == 263)
                    {
                        toolTip.ToolTipIcon = ToolTipIcon.Error;
                        toolTip.Show("Recording had occured improperly.\n" + "Please try recording again, then press play.", Is_Recording_Playing_CheckBox);
                        toolTip.Show("Recording had occured improperly.\n" + "Please try recording again, then press play.", Is_Recording_Playing_CheckBox);
                        return;
                    }

                    if (recording_player.IsPlaying)
                    {
                        Is_Recording_Playing_CheckBox.Checked = true;
                        Playing_Position_TrackBar.Enabled = true;
                        Tick.Start();
                        Play_Recorded_Button.Text = "Pause";
                    }
                }

                #region Disable Unusable Controls
                    Recording_Hour_TextBox.Enabled = false;
                    Recording_Minute_TextBox.Enabled = false;
                    Recording_Second_TextBox.Enabled = false;
                    Record_with_Timer_Button.Enabled = false;
                    Start_Recording_Button.Enabled = false;
                    Stop_Recording_Button.Enabled = false;

                    Delete_Recorded_Button.Enabled = false;

                    Recording_Location_Textbox.Enabled = false;
                    Browse_Recording_Location.Enabled = false;
                    Save_Recording_Button.Enabled = false;
                #endregion

                Test_Recording_GroupBox.ForeColor = Color.Blue;
                Save_GroupBox.ForeColor = Color.Red;
            }
            private void Stop_Recorded_Button_Click(object sender, EventArgs e)
            {
                if (File.Exists(initial_rec_location))
                {
                    recording_player.Stop();
                    recording_player.Close();
                }

                if (!recording_player.IsPlaying)
                {
                    Is_Recording_Playing_CheckBox.Checked = false;
                    Tick.Stop();
                    Playing_Position_TrackBar.Value = 0;
                }

                #region Enable Unusable Controls
                Recording_Hour_TextBox.Enabled = true;
                Recording_Minute_TextBox.Enabled = true;
                Recording_Second_TextBox.Enabled = true;
                Record_with_Timer_Button.Enabled = true;
                Start_Recording_Button.Enabled = true;
                Stop_Recording_Button.Enabled = true;

                Delete_Recorded_Button.Enabled = true;

                Recording_Location_Textbox.Enabled = true;
                Browse_Recording_Location.Enabled = true;
                Save_Recording_Button.Enabled = true;

                Play_Recorded_Button.Text = "Play";
                #endregion
            }
            private void Tick_Tick(object sender, EventArgs e)
            {
                int pos = (recording_player.PositionInMS * Playing_Position_TrackBar.Maximum) / recording_player.DurationInMS;
                Playing_Position_TrackBar.Value = pos;

                if (Playing_Position_TrackBar.Value == Playing_Position_TrackBar.Maximum)
                {
                    Stop_Recorded_Button.PerformClick();
                    Playing_Position_TrackBar.Enabled = false;
                }
            }
            private void Playing_Position_TrackBar_Scroll(object sender, EventArgs e)
            {
                int pos = (this.Playing_Position_TrackBar.Value * recording_player.DurationInMS) / Playing_Position_TrackBar.Maximum;
                recording_player.ChangePosition(pos);
            }

            private void Browse_Recording_Location_Click(object sender, EventArgs e)
            {
                saveFileDialog1.ShowDialog();
            }
            public void Delete_Recorded_Button_Click(object sender, EventArgs e)
            {
                if (File.Exists(initial_rec_location))
                {
                    File.Delete(initial_rec_location);
                }

                Play_Recorded_Button.Enabled = false;
                Stop_Recorded_Button.Enabled = false;
                Delete_Recorded_Button.Enabled = false;
                Save_Recording_Button.Enabled = false;

                Test_Recording_GroupBox.ForeColor = Color.Blue;
                Start_Recording_GroupBox.ForeColor = Color.Red;

                if (Save_GroupBox.ForeColor == Color.Red)
                {
                    Save_GroupBox.ForeColor = Color.Blue;
                }
            } 
            public void Save_Recording_Button_Click(object sender, EventArgs e)
            {
                Recording_Location_Textbox.Text = Recording_Location_Textbox.Text.Replace(".wav", "");

                if (Recording_Location_Textbox.Text != "" && Compress_checkBox.Checked == false)
                {
                    #region Disable Unusable Controls
                    Start_Recording_GroupBox.Enabled = false;
                    Test_Recording_GroupBox.Enabled = false;
                    Save_GroupBox.Enabled = false;
                    #endregion

                    #region Create Indicating Label

                    saving_label = new Label();

                    saving_label.Text = (Compress_checkBox.CheckState == CheckState.Checked) ? ("Saving and Compressing the Recording...") : ("Saving the Recording...");
                    saving_label.Location = new Point(21, 191);
                    saving_label.Size = new Size(237, 25);
                    saving_label.Show();

                    Sound_Recorder_TabPage.Controls.Add(saving_label);

                    #endregion
                    #region 
                        FileStream audio = File.Open(Recording_Location_Textbox.Text+".wav", FileMode.Create);
                        byte[] old_audio_data = File.ReadAllBytes(initial_rec_location);
                        audio.Write(old_audio_data, 0, old_audio_data.Length);
                        audio.Close();

                        Save_Recording_Button.Enabled = false;

                        Save_GroupBox.ForeColor = Color.Blue;
                        Start_Recording_GroupBox.ForeColor = Color.Red;

                        if (Test_Recording_GroupBox.ForeColor == Color.Red)
                        {
                            Test_Recording_GroupBox.ForeColor = Color.Blue;
                        } 
                    #endregion

                    #region Enable Unuseable Controls
                    Start_Recording_GroupBox.Enabled = true;
                    Test_Recording_GroupBox.Enabled = true;
                    Save_GroupBox.Enabled = true;
                    #endregion
                }

                if (Recording_Location_Textbox.Text == "")
                {
                    Invalid_Location_ToolTip.SetToolTip(Recording_Location_Textbox, "Please put a valid saving address in this text.\n" + "Press the 'Browse' buttton to find a proper saving address.");
                }

                if (Compress_checkBox.Checked && Recording_Location_Textbox.Text != "")
                {
                    #region Disable Unusable Controls
                        Start_Recording_GroupBox.Enabled = false;
                        Test_Recording_GroupBox.Enabled = false;
                        Save_GroupBox.Enabled = false;
                    #endregion

                    #region Create Indicating Label

                    saving_label = new Label();

                    saving_label.Text = "Saving and Compressing the Recording...";
                    saving_label.Location = new Point(21, 191);
                    saving_label.Size = new Size(237, 25);
                    saving_label.Show();

                    Sound_Recorder_TabPage.Controls.Add(saving_label);

                    #endregion
                    #region Convert Recording

                    Set_Format_State();
                    string source = initial_rec_location;
                    string destination = Recording_Location_Textbox.Text.Replace(".", ""); Recording_Location_Textbox.Text += "_c" + "." + newConvertingFormat_fileExtension; destination = Recording_Location_Textbox.Text;
                    previously_converted = Recording_Location_Textbox.Text;
                    if (File.Exists(destination))
                    {
                        File.Delete(destination);
                    }
                    StringCollection locations = new StringCollection(); locations.Add(source); locations.Add(destination);

                    Auto_Convert(locations);
                   
                    #endregion
                }

                if (Add_to_Playlist_CheckBox.Checked && Recording_Location_Textbox.Text != "")
                {
                    #region
                        if ((Recording_Playlist_Name.Text != "" || Recording_Location_Textbox.Text != "") && !W_M_Playlist_Dictionary.ContainsKey(Recording_Playlist_Name.Text.ToLower()) && !W_M_Playlist_Dictionary.ContainsValue(Recording_Location_Textbox.Text.ToLower()))
                        {
                            Windows_Media_Name_ComboBox.Items.Add(Recording_Playlist_Name.Text.ToLower());

                            if (Compress_checkBox.Checked)
                            {
                                W_M_Playlist_Dictionary.Add(Recording_Playlist_Name.Text.ToLower(), Recording_Location_Textbox.Text.ToLower());
                            }
                            else
                            {
                                W_M_Playlist_Dictionary.Add(Recording_Playlist_Name.Text.ToLower(), Recording_Location_Textbox.Text.ToLower()+".wav");
                            }

                            Recording_Playlist_Name.Text = "Add Playlist Name";
                            Recording_Location_Textbox.Text = "";
                        }

                        if (W_M_Playlist_Dictionary.ContainsKey(Recording_Playlist_Name.Text))
                        {
                            Invalid_Name_ToolTip.Show("Type in Another Name or Uncheck 'Add to Playlist'", Recording_Playlist_Name);
                        }
                        if (W_M_Playlist_Dictionary.ContainsValue(Recording_Location_Textbox.Text))
                        {
                            Invalid_Location_ToolTip.Show("Type in Another Name or Uncheck 'Add to Playlist'", Recording_Location_Textbox);
                        } 
                    #endregion
                    #region
                        W_M_keyValueItems.Clear();
                        foreach (string key in W_M_Playlist_Dictionary.Values)
                        {
                            W_M_keyValueItems.Add(key);
                        }
                        if (Windows_Media_Playlist_ComboBox.Items.Count != Windows_Media_Name_ComboBox.Items.Count)
                        {
                            Windows_Media_Playlist_ComboBox.Items.Clear();

                            foreach (string item in W_M_Playlist_Dictionary.Keys)
                            {
                                Windows_Media_Playlist_ComboBox.Items.Add(item);
                            }
                        }
                        total_W_M_tracks = W_M_Playlist_Dictionary.Count;
                    #endregion
                }
            }
            public void Save_Recording_Button_Click(int saving_num, string sched_name)
            {
                //Recording_Location_Textbox.Text = Recording_Location_Textbox.Text.Replace(".wav", "") + "_Sched" + Convert.ToString(saving_num);
                Recording_Location_Textbox.Text = scheduleSaveDirectory + "\\"+sched_name+"_"+Convert.ToString(saving_num);

                if (Recording_Location_Textbox.Text != "" && Compress_checkBox.Checked == false)
                {
                    #region Disable Unusable Controls
                    Start_Recording_GroupBox.Enabled = false;
                    Test_Recording_GroupBox.Enabled = false;
                    Save_GroupBox.Enabled = false;
                    #endregion 


                    #region Create Indicating Label

                    saving_label = new Label();

                    saving_label.Text = "Saving...PLEASE WAIT...";
                    saving_label.Location = new Point(21, 191);
                    saving_label.Size = new Size(237, 25);
                    saving_label.Show();

                    Sound_Recorder_TabPage.Controls.Add(saving_label);

                    #endregion
                    

                    #region
                    FileStream audio = File.Open(Recording_Location_Textbox.Text+".wav", FileMode.Create);
                    byte[] old_audio_data = File.ReadAllBytes(initial_rec_location);
                    audio.Write(old_audio_data, 0, old_audio_data.Length);
                    audio.Close();

                    #region Open Created File

                    Process proc = new Process();
                    proc.StartInfo.FileName = scheduleSaveDirectory.ToString();
                    proc.Start();

                    #endregion

                    Save_Recording_Button.Enabled = false;

                    Save_GroupBox.ForeColor = Color.Blue;
                    Start_Recording_GroupBox.ForeColor = Color.Red;

                    if (Test_Recording_GroupBox.ForeColor == Color.Red)
                    {
                        Test_Recording_GroupBox.ForeColor = Color.Blue;
                    }
                    #endregion


                    #region Enable Unuseable Controls
                    Start_Recording_GroupBox.Enabled = true;
                    Test_Recording_GroupBox.Enabled = true;
                    Save_GroupBox.Enabled = true;
                    #endregion
                }

                if (Compress_checkBox.Checked && Recording_Location_Textbox.Text != "")
                {
                    #region Disable Unusable Controls
                    Start_Recording_GroupBox.Enabled = false;
                    Test_Recording_GroupBox.Enabled = false;
                    Save_GroupBox.Hide();
                    #endregion

                    #region Create Indicating Label

                    saving_label = new Label();

                    saving_label.Text = "Saving and Compressing...PLEASE WAIT...";
                    saving_label.Location = new Point(21, 191);
                    saving_label.Size = new Size(237, 25);
                    saving_label.Show();

                    Sound_Recorder_TabPage.Controls.Add(saving_label);

                    #endregion
                    #region Convert Recording

                    Set_Format_State();
                    string source = initial_rec_location;
                    string destination = Recording_Location_Textbox.Text.Replace(".", ""); Recording_Location_Textbox.Text += "._c" + "." + newConvertingFormat_fileExtension; destination = Recording_Location_Textbox.Text;
                    previously_converted = Recording_Location_Textbox.Text;
                    if (File.Exists(destination))
                    {
                        File.Delete(destination);
                    }
                    StringCollection locations = new StringCollection(); locations.Add(source); locations.Add(destination);

                    Auto_Convert(locations);
                    #endregion
                }
            }
            private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
            {
                Recording_Location_Textbox.Text = saveFileDialog1.FileName;
            }
            private void Add_to_Playlist_CheckBox_CheckedChanged(object sender, EventArgs e)
            {
                if (Add_to_Playlist_CheckBox.Checked)
                {
                    Recording_Playlist_Name.Enabled = true;
                    Recording_Playlist_Name.Text = "Add Playlist Name";
                }

                if (!Add_to_Playlist_CheckBox.Checked)
                {
                    Recording_Playlist_Name.Enabled = false;
                    Recording_Playlist_Name.Text = "";
                }
            }

            private void Recording_Playlist_Name_MouseClick(object sender, MouseEventArgs e)
            {
                if (Recording_Playlist_Name.Text == " " || Recording_Playlist_Name.Text == "  " || Recording_Playlist_Name.Text == "Add Playlist Name")
                {
                    Recording_Playlist_Name.Text = "";
                }
            }
            private void Recording_Playlist_Name_MouseLeave(object sender, EventArgs e)
            {
                if (Recording_Playlist_Name.Text == "" || Recording_Playlist_Name.Text == " ")
                {
                    Recording_Playlist_Name.Text = "Add Playlist Name";
                }
            }
        #endregion

        #region Converter
            private void Browse_Conversion_Source_Button_Click(object sender, EventArgs e)
            {
                openFileDialog3.ShowDialog();

                #region User Guide
                    Select_groupBox.ForeColor = Color.Blue;
                    Format_groupBox.ForeColor = Color.Red;
                    Destination_groupBox.ForeColor = Color.Blue;
                    Convert_groupBox.ForeColor = Color.Blue;
                #endregion
            }        
            private void openFileDialog3_FileOk(object sender, CancelEventArgs e)
            {
                Conversion_Source_textBox.Text = openFileDialog3.FileName;
            }

            private void Browse_Conversion_Destination_button_Click(object sender, EventArgs e)
            {
                saveFileDialog2.ShowDialog();

                #region User Guide
                Select_groupBox.ForeColor = Color.Blue;
                Format_groupBox.ForeColor = Color.Blue;
                Destination_groupBox.ForeColor = Color.Blue;
                Convert_groupBox.ForeColor = Color.Red;
                #endregion
            }
            private void saveFileDialog2_FileOk(object sender, CancelEventArgs e)
            {
                Conversion_Destination_textBox.Text = saveFileDialog2.FileName;
            }

            public void Set_Format_State()
            {
                switch ((string)Format_List_comboBox.SelectedItem)
                {
                    case ".MP3":
                        is_convertingFormat_MP3 = true;
                        is_convertingFormat_CompressedWav = false;
                        newConvertingFormat_fileExtension = "mp3";
                        break;
                    case "Compressed .Wav":
                        is_convertingFormat_MP3 = false;
                        is_convertingFormat_CompressedWav = true;
                        newConvertingFormat_fileExtension = "wav";
                        break;
                    default:
                        is_convertingFormat_MP3 = true;
                        is_convertingFormat_CompressedWav = false;
                        newConvertingFormat_fileExtension = "mp3";
                        break;
                }
            }
            public void Set_Converting_radioButton_State(object state)
            {
                if (this.Is_Converting_radioButton.InvokeRequired)
                {
                    SetState set = new SetState(Set_Converting_radioButton_State);

                    this.Invoke(set, state);
                }
                else
                {
                    Is_Converting_radioButton.Checked = (bool)state;
                }
            }

            public void Converting_Error()
            {
                if (this.Convert_button.InvokeRequired)
                {
                    SetControl set = new SetControl(Converting_Error);
                    this.Invoke(set);
                }
                else
                {
                    MessageBox.Show(this, "SORRY. The conversion process could not proceed.\n\n" + "Try:\n" + "   1. Choosing a another file to convert\n" + "   2. Choosing another format", "Conversion Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            private void Format_List_comboBox_Click(object sender, EventArgs e)
            {
                #region User Guide
                Select_groupBox.ForeColor = Color.Blue;
                Format_groupBox.ForeColor = Color.Blue;
                Destination_groupBox.ForeColor = Color.Red;
                Convert_groupBox.ForeColor = Color.Blue;
                #endregion
            }
            private void Format_List_comboBox_DropDownClosed(object sender, EventArgs e)
            {
                Set_Format_State();
            }

            private void Convert_button_Click(object sender, EventArgs e)
            {
                if (Conversion_Destination_textBox.Text != "" && Conversion_Source_textBox.Text != "")
                {
                    #region Disable Unusable Controls
                    Select_groupBox.Enabled = false;
                    Format_groupBox.Enabled = false;
                    Destination_groupBox.Enabled = false;
                    Convert_groupBox.Hide();
                    #endregion

                    #region Create Indicators
                    convert_label = new Label();

                    convert_label.Text = "Converting........Please Wait";
                    convert_label.Location = new Point(80, 182);
                    convert_label.Size = new Size(225, 22);

                    Converter_tabPage.Controls.Add(convert_label);

                    c_progress = new Converting_Progress();
                    c_progress.Show();
                    c_progress.Activate();

                    if (isScheduling)
                    {
                        c_progress.schedFileName = scheduledFileName; 
                    }

                    #endregion

                    #region Initiate Conversion

                    Is_Converting_radioButton.Checked = true;

                    Set_Format_State();
                    Conversion_Destination_textBox.Text += "." + newConvertingFormat_fileExtension;
                    if (File.Exists(Conversion_Destination_textBox.Text))
                    {
                        File.Delete(Conversion_Destination_textBox.Text);
                    }

                    StringCollection locations = new StringCollection(); locations.Add(Conversion_Source_textBox.Text); locations.Add(Conversion_Destination_textBox.Text);
                    Convert_backgroundWorker.RunWorkerAsync(locations);

                    previously_converted = Conversion_Destination_textBox.Text;

                    //Is_Converting_radioButton.Checked = false;

                    #endregion

                    #region Enable Unusable Controls
                    Select_groupBox.Enabled = true;
                    Format_groupBox.Enabled = true;
                    Destination_groupBox.Enabled = true;
                    #endregion

                    #region User Guide
                    Select_groupBox.ForeColor = Color.Red;
                    Format_groupBox.ForeColor = Color.Blue; ;
                    Select_groupBox.ForeColor = Color.Blue;
                    Convert_groupBox.ForeColor = Color.Blue;
                    #endregion

                    Delete_Converted_Button.Enabled = true;
                    Conversion_Source_textBox.Text = "";
                    Conversion_Destination_textBox.Text = ""; 
                }
            }
            private void Auto_Convert(StringCollection loc)
            {
                if (true)
                {
                    #region Disable Unusable Controls
                    Select_groupBox.Enabled = false;
                    Format_groupBox.Enabled = false;
                    Destination_groupBox.Enabled = false;
                    Convert_groupBox.Hide();
                    #endregion

                    #region Create Indicators
                    convert_label = new Label();

                    convert_label.Text = "Converting........Please Wait";
                    convert_label.Location = new Point(80, 182);
                    convert_label.Size = new Size(225, 22);

                    Converter_tabPage.Controls.Add(convert_label);

                    c_progress = new Converting_Progress();
                    c_progress.Show();
                    c_progress.Activate();

                    if (isScheduling)
                    {
                        c_progress.schedFileName = scheduledFileName;
                    }

                    #endregion

                    #region Initiate Conversion

                    Is_Converting_radioButton.Checked = true;

                    Convert_backgroundWorker.RunWorkerAsync(loc);

                    //Is_Converting_radioButton.Checked = false;

                    #endregion

                    #region Enable Unusable Controls
                    Select_groupBox.Enabled = true;
                    Format_groupBox.Enabled = true;
                    Destination_groupBox.Enabled = true;
                    #endregion

                    #region User Guide
                    Select_groupBox.ForeColor = Color.Red;
                    Format_groupBox.ForeColor = Color.Blue; ;
                    Select_groupBox.ForeColor = Color.Blue;
                    Convert_groupBox.ForeColor = Color.Blue;
                    #endregion

                    Delete_Converted_Button.Enabled = true;
                    Conversion_Source_textBox.Text = "";
                    Conversion_Destination_textBox.Text = "";
                }
            }
            private void Start_Converting(object sender, DoWorkEventArgs e)
            {
                StringCollection s = (StringCollection)e.Argument; string source = s[0];
                StringCollection d = (StringCollection)e.Argument; string destination = s[1];

                #region Initiate Progress Indicator

                if(Schedule_Recording_checkBox.Checked == true)
                {
                    c_progress.is_scheduled = true;
                    c_progress.conversionDirectory = scheduleSaveDirectory;
                }
                
                #endregion

                try
                {
                    #region Convert to Compressed Wav

                    if (is_convertingFormat_CompressedWav == true)
                    {
                        Set_Converting_radioButton_State(true);
                        FileStream fs_r = new FileStream(source, FileMode.Open);

                        using (AudioReader ar = new WaveReader(fs_r))
                        {
                            IntPtr oldFormat = ar.ReadFormat();

                            int read_size = ar.Milliseconds2Bytes(1000);
                            int total_length = ar.GetLengthInBytes();
                            int current_progress = 0;
                            double percentChange = (double)total_length / (double)read_size;

                            //this.newConvertingFormat = AudioCompressionManager.GetCompatibleFormat(oldFormat, AudioCompressionManager.AdpcmFormatTag);
                            this.newConvertingFormat = AudioCompressionManager.GetPcmFormat(2, 8, 44100);

                            FileStream fs_w = new FileStream(destination, FileMode.Create);
                            if (true)
                            {
                                c_progress.Conversion_Progressbar.Maximum = total_length;
                            }

                            using (Alvas.Audio.WaveWriter ww = new Alvas.Audio.WaveWriter(fs_w, AudioCompressionManager.FormatBytes(newConvertingFormat)))
                            {
                                Cursor.Current = Cursors.WaitCursor;
                                AcmConverter ac = new AcmConverter(oldFormat, this.newConvertingFormat, false);
                                while (current_progress < total_length)
                                {
                                    Application.DoEvents();
                                    byte[] data = ar.ReadDataInBytes(current_progress, read_size);
                                    current_progress += data.Length;
                                    byte[] newdata = ac.Convert(data);
                                    ww.WriteData(newdata);

                                    c_progress.Conversion_Progressbar.Value = current_progress;
                                    c_progress.Conversion_Progressbar.Refresh();

                                    //convert_toolTip.SetToolTip(c_progress, string.Format("{0}% converted", (current_progress/total_length)));
                                    //c_progress.Text = string.Format("Conversion Progress : {0}%", (current_progress/total_length));
                                    Application.DoEvents();
                                }
                                fs_r.Close();
                                ar.Close();
                                fs_w.Close();
                                ww.Close();
                                ac.Close();
                                Cursor.Current = Cursors.WaitCursor;
                            }
                        }

                        Set_Converting_radioButton_State(false);
                    }

                    #endregion
                    #region Convert to MP3
                    if (is_convertingFormat_MP3 == true)
                    {
                        Set_Converting_radioButton_State(true);
                        WaveStream ws_r = new WaveStream(source);

                        
                        Mp3WriterConfig mp3_config = new Mp3WriterConfig(ws_r.Format);
                        Yeti.MMedia.Mp3.Mp3Writer mp3_w = new Yeti.MMedia.Mp3.Mp3Writer(new FileStream(destination, FileMode.Create), mp3_config);

                        int total_length = 100;
                        int current_progress = 0;
                        byte[] buff = new byte[mp3_w.OptimalBufferSize];
                        int read = 0;
                        int actual = 0;

                        while ((read = ws_r.Read(buff, 0, buff.Length)) > 0)
                        {
                            current_progress = total_length;

                            //Do Conversion
                            mp3_w.Write(buff, 0, read);
                            actual += read;
                            // 
                        }
                        mp3_w.Close();
                        ws_r.Close();

                        //c_progress.Dispose();
                        Set_Converting_radioButton_State(false);
                    }
                    #endregion
                }
                catch (Exception error)
                {
                    c_progress.Dispose();
                    MessageBox.Show(error.ToString());
                    //Converting_Error();
                    return;
                }
            }
            private void Convert_Completed(object sender, RunWorkerCompletedEventArgs e)
            {
                try
                {
                    if (convert_label != null)
                    {
                        convert_label.Dispose();
                        c_progress.showConfirmation();
                        //c_progress.Dispose();

                        Convert_groupBox.Show();

                        #region User Guide
                        Select_groupBox.ForeColor = Color.Red;
                        Format_groupBox.ForeColor = Color.Blue;
                        Destination_groupBox.ForeColor = Color.Blue;
                        Convert_groupBox.ForeColor = Color.Blue;
                        #endregion
                    }
                    if (true)
                    {
                        #region Finalize Save
                        //string old_file_location = Recording_Location_Textbox.Text;
                        //if (File.Exists(old_file_location))
                        //{
                        //    File.Delete(old_file_location);
                        //}
                        #endregion
                        #region Finalize
                        if (Recording_Location_Textbox.Text != "")
                        {
                            Delete_Recorded_Button.PerformClick();

                            Recording_Location_Textbox.Text = "";
                            Compress_checkBox.Checked = false;
                            Add_to_Playlist_CheckBox.Checked = false;

                            Sound_Recorder_TabPage.Controls.Remove(saving_label);
                            Save_GroupBox.Show();
                        }
                        #endregion
                        #region Enable Unusable Controls
                        Start_Recording_GroupBox.Enabled = true;
                        Test_Recording_GroupBox.Enabled = true;
                        Save_GroupBox.Enabled = true;
                        #endregion
                    }  
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.ToString());
                }  
            }
            private void Delete_Converted_Button_Click(object sender, EventArgs e)
            {
                string destination = previously_converted;

                Delete_Converted_Button.Enabled = false;

                if (File.Exists(destination))
                {
                    File.Delete(destination);
                }

                #region User Guide
                Select_groupBox.ForeColor = Color.Red;
                Format_groupBox.ForeColor = Color.Blue;
                Destination_groupBox.ForeColor = Color.Blue;
                Convert_groupBox.ForeColor = Color.Blue;
                #endregion
            }
        #endregion

        private void Save_Playlists()
        {
            Settings.Default.Web_Names.Clear();
            foreach (string item in Web_Playlist_Dictionary.Keys)
            {
                Settings.Default.Web_Names.Add(item);
            }

            Settings.Default.Web_Locations.Clear();
            foreach (string item in Web_Playlist_Dictionary.Values)
            {
                Settings.Default.Web_Locations.Add(item);
            }

            Settings.Default.W_M_Names.Clear();
            foreach (string item in W_M_Playlist_Dictionary.Keys)
            {
                Settings.Default.W_M_Names.Add(item);
            }

            Settings.Default.W_M_Locations.Clear();
            foreach (string item in W_M_Playlist_Dictionary.Values)
            {
                Settings.Default.W_M_Locations.Add(item);
            }

            Settings.Default.Main_Button_Names.Clear();
            foreach (string item in Main_Button_Playlist_Dictionary.Keys)
            {
                Settings.Default.Main_Button_Names.Add(item);
            }

            Settings.Default.Main_Button_Locations.Clear();
            foreach (string item in Main_Button_Playlist_Dictionary.Values)
            {
                Settings.Default.Main_Button_Locations.Add(item);
            }

            Settings.Default.Save();
        }

        private void My_Player_FormClosing(object sender, FormClosingEventArgs e)
        {
            Delete_Recorded_Button_Click(null, null);

            Save_Playlists();
        }
    }
}
