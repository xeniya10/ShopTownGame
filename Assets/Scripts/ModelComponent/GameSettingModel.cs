namespace ShopTown.ModelComponent
{
public enum Settings { Music, Sound, Notifications, Ads };

public class GameSettingModel
{
    public bool MusicOn;
    public bool SoundOn;
    public bool NotificationsOn;
    public bool AdsOn;

    public void ChangeState(Settings parameter)
    {
        switch (parameter)
        {
            case Settings.Music:
                ChangeMusicState();
                break;

            case Settings.Sound:
                ChangeSoundState();
                break;

            case Settings.Notifications:
                ChangeNotificationsState();
                break;

            case Settings.Ads:
                ChangeAdsState();
                break;
        }
    }

    private void ChangeMusicState()
    {
        if (MusicOn)
        {
            MusicOn = false;
            return;
        }

        MusicOn = true;
    }

    private void ChangeSoundState()
    {
        if (SoundOn)
        {
            SoundOn = false;
            return;
        }

        SoundOn = true;
    }

    private void ChangeNotificationsState()
    {
        if (NotificationsOn)
        {
            NotificationsOn = false;
            return;
        }

        NotificationsOn = true;
    }

    private void ChangeAdsState()
    {
        if (AdsOn)
        {
            AdsOn = false;
        }
    }
}
}
