using HMExtension.Maui;
using HMPopup;

namespace HMControls;

public static class MauiProgram
{

    public static MauiAppBuilder UseHMControls(this MauiAppBuilder builder)
    {
        builder
            .UseHMPopup();
        return builder;
    }
}
