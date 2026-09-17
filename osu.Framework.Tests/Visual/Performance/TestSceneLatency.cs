// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Configuration;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Input.Events;
using osu.Framework.Platform;
using osu.Framework.Testing;
using osuTK.Graphics;
using Veldrid;

namespace osu.Framework.Tests.Visual.Performance
{
    public partial class TestSceneLatency : TestScene
    {
        private GameHost host = null!;

        private Bindable<double> maxFps = null!;
        private Bindable<FrameSync> frameSync = null!;
        private Bindable<LowLatency> lowLatency = null!;
        private Bindable<ExecutionMode> executionMode = null!;

        private SpriteText fpsText = null!;
        private SpriteText maximumDrawHz = null!;
        private SpriteText maximumUpdateHz = null!;
        private SpriteText frameSyncText = null!;
        private SpriteText lowLatencyText = null!;
        private SpriteText executionModeText = null!;

        [BackgroundDependencyLoader]
        private void load(FrameworkConfigManager config, GameHost host)
        {
            this.host = host;

            Children =
            [
                new FlashingBox {},
                new FillFlowContainer
                {
                    RelativeSizeAxes = Axes.X,
                    AutoSizeAxes = Axes.Y,
                    Direction = FillDirection.Vertical,
                    Spacing = new osuTK.Vector2(0, 5),
                    Children =
                    [
                        new SpriteText { Text = "Click anywhere to flash the screen white.", },
                        fpsText = new SpriteText {},
                        maximumDrawHz = new SpriteText {},
                        maximumUpdateHz = new SpriteText {},
                        frameSyncText = new SpriteText {},
                        lowLatencyText = new SpriteText {},
                        executionModeText = new SpriteText {},
                    ]
                },
            ];

            maxFps = config.GetBindable<double>(FrameworkSetting.MaxFps);
            frameSync = config.GetBindable<FrameSync>(FrameworkSetting.FrameSync);
            lowLatency = config.GetBindable<LowLatency>(FrameworkSetting.LowLatency);
            executionMode = config.GetBindable<ExecutionMode>(FrameworkSetting.ExecutionMode);

            AddSliderStep("Max FPS", 0, 1000, maxFps.Value, v => maxFps.Value = v);
            AddStep("Frame sync: Off", () => frameSync.Value = FrameSync.Off);
            AddStep("Frame sync: VSync", () => frameSync.Value = FrameSync.VSync);
            AddStep("Frame sync: VRR", () => frameSync.Value = FrameSync.VSyncVRR);
            AddStep("Low latency: Off", () => lowLatency.Value = LowLatency.Off);
            AddStep("Low latency: On", () => lowLatency.Value = LowLatency.On);
            AddStep("Low latency: OnWithBoost", () => lowLatency.Value = LowLatency.OnWithBoost);
            AddStep("Execution mode: MultiThreaded", () => executionMode.Value = ExecutionMode.MultiThreaded);
            AddStep("Execution mode: SingleThread", () => executionMode.Value = ExecutionMode.SingleThread);
        }

        protected override void Update()
        {
            fpsText.Text = $"Maximum FPS: {maxFps?.Value:N2}";
            maximumDrawHz.Text = $"Maximum Draw Hz: {host.MaximumDrawHz:N2}";
            maximumUpdateHz.Text = $"Maximum Update Hz: {host.MaximumUpdateHz:N2}";
            frameSyncText.Text = $"Frame sync: {frameSync?.Value}";
            lowLatencyText.Text = $"Low latency: {host.Renderer?.LowLatencyMode ?? LowLatencyMode.Off}";
            executionModeText.Text = $"Execution mode: {executionMode?.Value}";
        }

        private partial class FlashingBox : Box
        {
            public FlashingBox()
            {
                RelativeSizeAxes = Axes.Both;
                Colour = Color4.Black;
            }

            protected override bool OnMouseDown(MouseDownEvent e)
            {
                Colour = Color4.White;
                return base.OnMouseDown(e);
            }

            protected override void OnMouseUp(MouseUpEvent e)
            {
                Colour = Color4.Black;
                base.OnMouseUp(e);
            }
        }
    }
}
