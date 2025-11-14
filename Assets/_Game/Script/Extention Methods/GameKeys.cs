
public static class GameKeys
{
    public const string SOUND_EFFECT_STATE_KEY = "SoundEffectState";
    public const string MUSIC_STATE_KEY = "MusicState";
    public const string NOTIFICATION_STATE_KEY = "NotificationState";
    public const string HAPTIC_STATE_KEY = "HapticState";
    public const string SELECTED_LANGUAGE_KEY = "LanguageIndex";
    public const string MUSIC_VOLUME_KEY = "Volume";
    public const string SOUND_VOLUME_KEY = "SoundVolume";

    public const string STORY_SHOWN_KEY = "StoryShown";

    public const string PROFILE_PICTURE_KEY = "ProfilePicture";

    public const string CURRENCY_COIN_KEY = "CN";
    public const string CURRENCY_GEM_KEY = "GM";
    public const string POWERUP_HEALER_KEY = "HEALERPOWERUP";
    public const string POWERUP_SPEED_KEY = "SPEEDPOWERUP";


    public const string CHARECTERINDEX = "CHARECTERINDEX";


#if AMAZON
      public const string STORE_URL = "https://www.amazon.com/gp/product/B0DCG42FYR";
#else

#if UNITY_ANDROID
    public const string STORE_URL = "https://play.google.com/store/apps/details?id=com.cocoboogames.ozo.oceanzombieoutbreak";
#elif UNITY_IOS
    public const string STORE_URL = "https://apps.apple.com/app/ozo-ocean-zombie-outbreak/id6602891043";
#endif
#endif

    public const string REVIEW_SUBMIT_KEY = "IsReviewSubmitted";
    public const string REVIEW_LEVEL_KEY = "ReviewLevel";


    public const string REMOTE_CONFIG_CURRENT_VERSION_KEY = "CurrentGameVersion";
    public const string REMOTE_CONFIG_CROSSPROMO_VIDEO_URL_KEY = "CrossPromotion_VideoUrl";
    public const string REMOTE_CONFIG_CROSSPROMO_ONCLICK_URL_KEY = "CrossPromotion_ClickUrl";
    public const string REMOTE_CONFIG_TERMS_KEY = "T&C";
    public const string REMOTE_CONFIG_PRIVACY_KEY = "Privacy";

    public const string REMOTE_CONFIG_INTERSTITIAL_AD_KEY = "InterstitialAD";
    public const string REMOTE_CONFIG_REWARDED_AD_KEY = "RewardedVideoAD";


    public const int DEFENDER_LAYER_INDEX = 3;
    public const int ATTACKER_LAYER_INDEX = 6;


    public const int MENU_SCENE_INDEX = 1;
    public const int GAME_SCENE_INDEX = 2;

    public const string GAME_SCENE_NAME = "Game";
    public const string MENU_SCENE_NAME = "Menu";
    public const string HEALTH_BAR_STATUS_KEY = "HealthBarStatus";

}
