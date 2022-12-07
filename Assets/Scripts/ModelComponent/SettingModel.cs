using System;

namespace ShopTown.ModelComponent
{
public enum Setting { Music, Sound, Notifications, Ads };

[Serializable]
public class SettingModel
{
    public bool MusicOn;
    public bool SoundOn;
    public bool NotificationsOn;
    public bool AdsOn;

    [NonSerialized] public Action ChangeEvent;

    public void SetDefaultData(SettingModel defaultData)
    {
        MusicOn = defaultData.MusicOn;
        SoundOn = defaultData.SoundOn;
        NotificationsOn = defaultData.NotificationsOn;
        AdsOn = defaultData.AdsOn;
    }

    public void ChangeState(Setting setting, out bool parameter)
    {
        parameter = false;

        switch (setting)
        {
            case Setting.Music:
                ChangeState(ref MusicOn);
                parameter = MusicOn;
                break;

            case Setting.Sound:
                ChangeState(ref SoundOn);
                parameter = SoundOn;
                break;

            case Setting.Notifications:
                ChangeState(ref NotificationsOn);
                parameter = NotificationsOn;
                break;

            case Setting.Ads:
                ChangeState(ref AdsOn);
                parameter = AdsOn;
                break;
        }

        ChangeEvent?.Invoke();
    }

    private void ChangeState(ref bool setting)
    {
        if (setting)
        {
            setting = false;
            return;
        }

        setting = true;
    }
}
}
