using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace EdgeBrowser
{
    public class Edge
    {
        /*
         * Configure_Favourites
         * Apply_OnstartUp
         * 
         **/
        private static Type m_type = Type.GetTypeFromProgID("WScript.Shell");
        private static object m_shell = Activator.CreateInstance(m_type);

        [ComImport, TypeLibType((short)0x1040), Guid("F935DC23-1CF0-11D0-ADB9-00C04FD58A0B")]
        private interface IWshShortcut
        {
            [DispId(0)]
            string FullName { [return: MarshalAs(UnmanagedType.BStr)][DispId(0)] get; }
            [DispId(0x3e8)]
            string Arguments { [return: MarshalAs(UnmanagedType.BStr)][DispId(0x3e8)] get; [param: In, MarshalAs(UnmanagedType.BStr)][DispId(0x3e8)] set; }
            [DispId(0x3e9)]
            string Description { [return: MarshalAs(UnmanagedType.BStr)][DispId(0x3e9)] get; [param: In, MarshalAs(UnmanagedType.BStr)][DispId(0x3e9)] set; }
            [DispId(0x3ea)]
            string Hotkey { [return: MarshalAs(UnmanagedType.BStr)][DispId(0x3ea)] get; [param: In, MarshalAs(UnmanagedType.BStr)][DispId(0x3ea)] set; }
            [DispId(0x3eb)]
            string IconLocation { [return: MarshalAs(UnmanagedType.BStr)][DispId(0x3eb)] get; [param: In, MarshalAs(UnmanagedType.BStr)][DispId(0x3eb)] set; }
            [DispId(0x3ec)]
            string RelativePath { [param: In, MarshalAs(UnmanagedType.BStr)][DispId(0x3ec)] set; }
            [DispId(0x3ed)]
            string TargetPath { [return: MarshalAs(UnmanagedType.BStr)][DispId(0x3ed)] get; [param: In, MarshalAs(UnmanagedType.BStr)][DispId(0x3ed)] set; }
            [DispId(0x3ee)]
            int WindowStyle { [DispId(0x3ee)] get; [param: In][DispId(0x3ee)] set; }
            [DispId(0x3ef)]
            string WorkingDirectory { [return: MarshalAs(UnmanagedType.BStr)][DispId(0x3ef)] get; [param: In, MarshalAs(UnmanagedType.BStr)][DispId(0x3ef)] set; }
            [TypeLibFunc((short)0x40), DispId(0x7d0)]
            void Load([In, MarshalAs(UnmanagedType.BStr)] string PathLink);
            [DispId(0x7d1)]
            void Save();
        }

        public static void Create(string fileName, string targetPath, string arguments, string workingDirectory, string description, string iconPath)
        {
            IWshShortcut shortcut = (IWshShortcut)m_type.InvokeMember("CreateShortcut", System.Reflection.BindingFlags.InvokeMethod, null, m_shell, new object[] { fileName });
            shortcut.Description = description;
            shortcut.TargetPath = targetPath;
            shortcut.WorkingDirectory = workingDirectory;
            shortcut.Arguments = arguments;
            if (!string.IsNullOrEmpty(iconPath))
                shortcut.IconLocation = iconPath;
            shortcut.Save();
        }
        public static void Configure()
        {
           
        }
        public static  void Configure_Chromiumfavorites()
        {
            /*
             * In this we will pass the list of favourite and add in registry path that is -  we need to write reg value in jason format
             * [{"children":[{"name":"Kadia","url":"https://www.google.com"}],"name":"Gokul"}] // format of Favourites
             * [{"children":[{"name":"yahoo","url":"www.yahoo.com"}],"name":"Goul_Test_FOlder"}]
             * Computer\HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Edge\RestoreOnStartupURLs = Value 1=https://www.google.com/ which ever we added in that
             */
            string MANAGED_PATH = @"SOFTWARE\Policies\Microsoft\Edge";
            string MANAGED_PATH_KEY = "ManagedFavorites";

        }

        public static void Apply_OnStartup()
        {
            /*
             1) in this function will set the registry of RestoreOnStartup based on On startup values from payload  //SOFTWARE\\Policies\\Microsoft\\Edge
             2) another registry key "SOFTWARE\\Policies\\Microsoft\\Edge\\RestoreOnStartupURLs" set this also 
             3) and last set the    HomepageLocation URL in registry 
             */
            string onStartup = "Homepage";  //comes from payload
            int homepageurl = 4, restore=1;
            if (onStartup == "Homepage")
            {
                //set registry key RestoreOnStartup
                //path= "HKLM\\SOFTWARE\\Policies\\Microsoft\\Edge" - set this RestoreOnStartup with size of homepageurl
                //after this set another registry value i.e.  "SOFTWARE\\Policies\\Microsoft\\Edge\\RestoreOnStartupURLs" with URLS
            }
            else if(onStartup == "Restore")
            {
                // set this value RestoreOnStartup with restore size
            }

            //at alst set the "HomepageLocation" with URL value 
        }

        public static void Apply_ChromiumApperance()
        {
            /*
             * //SOFTWARE\\Policies\\Microsoft\\Edge = Path
             * Setting the registry for enabling the FavoritesBarEnabled,EdgeCollectionsEnabled,UserFeedbackAllowed,ConfigureShare,ShowHomeButton,HardwareAccelerationModeEnabled
             */
        }

        public static void Apply_ChromiumPrivacy()
        {
            /*
             *  //SOFTWARE\\Policies\\Microsoft\\Edge = Path
             * in this same we need to set the registry 
             * TrackingPrevention in this Off=0, Basic = 1, Balanced = 2, Strict = 3 from Payload
             * ConfigureDoNotTrack Key we need to saet 0 or 1 or No and yes
             * 
             * Path=SOFTWARE\\Policies\\Microsoft\\Edge\\AllowTrackingForUrls
             * need to add Tracking URL from payload based on for loop we need to create key 0,1,2,3 ... and value URLs
             * 
             * 
             */
        }

        public static void CreateShortcutEdge_Favourites()
        {
            //based on the array from payload of favourites we need to use loop 
            // get the path from x86 =  0x002a 
            // from this we need to create shortcut 
            // process= C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe
            Create("C:\\Users\\Admin\\Desktop\\test.lnk", "C:\\Program Files (x86)\\Microsoft\\Edge\\Application\\msedge.exe", "www.google.com", "", "google.com", "");
        }

        public static void CreateTrustedSites()
        {
            // Computer\HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Internet Settings\ZoneMap\Domains
            //this is main registry to handle the trusted sites settings 
            //in above resitry we need to create registry key which ever we recieved from payload like = www.yahoo.com
            //so in registry - Computer\HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Internet Settings\ZoneMap\Domains\yahoo.com - create this
            //in the same path create one more key=  www and in that create Dword with KeyName=* and  value 2 

        }
    }

}
