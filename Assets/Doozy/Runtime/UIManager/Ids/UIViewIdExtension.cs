// Copyright (c) 2015 - 2023 Doozy Entertainment. All Rights Reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement
// A Copy of the EULA APPENDIX 1 is available at http://unity3d.com/company/legal/as_terms

//.........................
//.....Generated Class.....
//.........................
//.......Do not edit.......
//.........................

using System.Collections.Generic;
// ReSharper disable All
namespace Doozy.Runtime.UIManager.Containers
{
    public partial class UIView
    {
        public static IEnumerable<UIView> GetViews(UIViewId.HomeScreen id) => GetViews(nameof(UIViewId.HomeScreen), id.ToString());
        public static void Show(UIViewId.HomeScreen id, bool instant = false) => Show(nameof(UIViewId.HomeScreen), id.ToString(), instant);
        public static void Hide(UIViewId.HomeScreen id, bool instant = false) => Hide(nameof(UIViewId.HomeScreen), id.ToString(), instant);

        public static IEnumerable<UIView> GetViews(UIViewId.Ingame id) => GetViews(nameof(UIViewId.Ingame), id.ToString());
        public static void Show(UIViewId.Ingame id, bool instant = false) => Show(nameof(UIViewId.Ingame), id.ToString(), instant);
        public static void Hide(UIViewId.Ingame id, bool instant = false) => Hide(nameof(UIViewId.Ingame), id.ToString(), instant);

        public static IEnumerable<UIView> GetViews(UIViewId.Initialization id) => GetViews(nameof(UIViewId.Initialization), id.ToString());
        public static void Show(UIViewId.Initialization id, bool instant = false) => Show(nameof(UIViewId.Initialization), id.ToString(), instant);
        public static void Hide(UIViewId.Initialization id, bool instant = false) => Hide(nameof(UIViewId.Initialization), id.ToString(), instant);
    }
}

namespace Doozy.Runtime.UIManager
{
    public partial class UIViewId
    {
        public enum HomeScreen
        {
            Acclimation,
            ChooseAvatar,
            Controls,
            Customisation,
            InfoPage,
            Loading,
            Loading2,
            LoadingScreen,
            LoggingIn,
            LoginFailed,
            LoginPage,
            LoginSuccessful,
            previewCharacter,
            SelectGender,
            SplashScreen
        }

        public enum Ingame
        {
            BottomBar,
            ChatPanel,
            exit,
            feedbackPanel,
            LocationPanel,
            ProfilePanel,
            ReactionPanel,
            session,
            sessionsPanel
        }

        public enum Initialization
        {
            LoadingScreen
        }    
    }
}
