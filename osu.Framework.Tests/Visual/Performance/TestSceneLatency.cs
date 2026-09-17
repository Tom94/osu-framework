// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Configuration;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Input.Events;
using osu.Framework.Platform;
using osu.Framework.Testing;
using osuTK.Graphics;

namespace osu.Framework.Tests.Visual.Performance
{
    public partial class TestSceneLatency : TestScene
    {
        private Box box = null!;

        private Bindable<double> maxFpsVSync = null!;
        private Bindable<double> maxFpsCustom = null!;

        [BackgroundDependencyLoader]
        private void load(FrameworkConfigManager config, GameHost host)
        {
            maxFpsVSync = config.GetBindable<double>(FrameworkSetting.MaxFpsVSync);
            maxFpsCustom = config.GetBindable<double>(FrameworkSetting.MaxFpsCustom);

            Child = box = new Box
            {
                RelativeSizeAxes = Axes.Both,
                Colour = Color4.Black,
            };

            AddSliderStep("Max FPS (VSync)", 0, 1000, maxFpsVSync.Value, v => maxFpsVSync.Value = Math.Min(v, host.Window.CurrentDisplayMode.Value.RefreshRate));
            AddSliderStep("Max FPS (Custom)", 0, 1000, maxFpsCustom.Value, v => maxFpsCustom.Value = v);
        }

        protected override bool OnMouseDown(MouseDownEvent e)
        {
            box.Colour = Color4.White;
            return base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseUpEvent e)
        {
            box.Colour = Color4.Black;
            base.OnMouseUp(e);
        }
    }
}
