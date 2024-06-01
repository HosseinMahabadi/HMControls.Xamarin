using HMExtension.Maui;
using HMPopup;

namespace HMControls;

public static class MauiProgram
{

    public static MauiAppBuilder ConfigureHMControls(this MauiAppBuilder builder)
    {
        builder
            .ConfigureHMPopup();
        return builder;
    }
}
