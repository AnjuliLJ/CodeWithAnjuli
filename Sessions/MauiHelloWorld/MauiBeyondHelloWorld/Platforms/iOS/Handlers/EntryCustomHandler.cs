using System;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using UIKit;

namespace MauiBeyondHelloWorld.Platforms.iOS.Handlers;

public class EntryCustomHandler : EntryHandler
{
    protected override void ConnectHandler(MauiTextField platformView)
    {
        base.ConnectHandler(platformView);
        platformView.BorderStyle = UITextBorderStyle.None;
    }
}
