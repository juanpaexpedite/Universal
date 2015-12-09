using Microsoft.Graphics.Canvas.Effects;
using Microsoft.UI.Composition.Toolkit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Universal.Managers;
using Windows.UI.Composition;
using Windows.UI.Xaml;
using static Universal.Managers.CompositionManager;

namespace Universal.Compositions
{
    public class Saturation : DependencyObject, IComposition
    {
        #region Element
        private UIElement element;
        private FrameworkElement frameworkElement;
        #endregion

        #region Source
        public string Source
        {
            get { return (string)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }
        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register(nameof(Source), typeof(string), typeof(Saturation), new PropertyMetadata(null));
        #endregion

        #region Composition
        private ContainerVisual visual;
        private Compositor compositor;
        private CompositionImageFactory imageFactory;
        #endregion

        #region Effect
        private string EffectSource = "EffectSource";
        private string SaturationEffect = "SaturationEffect";
        private string SaturationEffectPath = "SaturationEffect.Saturation";
        private SaturationEffect effect;
        private CompositionEffectFactory effectFactory;
        #endregion

        #region CompositionImage
        private CompositionSurfaceBrush surfaceBrush;
        private CompositionImage imageSource;
        #endregion

        #region Brush & Sprite
        private CompositionEffectBrush effectBrush;
        private SpriteVisual spriteVisual;
        #endregion

        public void Initialize(UIElement attachedelement)
        {
            element = attachedelement;
            frameworkElement = element as FrameworkElement;

            CreateImageFactory(element);

            CreateEffect();

            CreateSurface();

            CreateBrush();

            CreateSpriteVisual();

            Insert();

            DetectElementChange();
        }

        #region Detect Properties Change
        private void DetectElementChange()
        {
            frameworkElement.SizeChanged += (s, e) =>
            {
                UpdateSpriteVisual();
            };
        }
        #endregion

        private void CreateImageFactory(UIElement element)
        {
            visual = GetVisual(element);
            compositor = GetCompositor(visual);
            imageFactory = CreateCompositionImageFactory(compositor);
        }

        private void CreateEffect()
        {
            effect = new SaturationEffect()
            {
                Name = SaturationEffect,
                Saturation = 0.0f,
                Source = new CompositionEffectSourceParameter(EffectSource)
            };

            UpdateLevel();
            effectFactory = compositor.CreateEffectFactory(effect, new[] { SaturationEffectPath });
        }

        private void CreateSurface()
        {
            surfaceBrush = compositor.CreateSurfaceBrush();
            var uriSource = UriManager.GetFilUriFromString(Source);
            imageSource = imageFactory.CreateImageFromUri(uriSource);
            surfaceBrush.Surface = imageSource.Surface;
        }

        private void CreateBrush()
        {
            effectBrush = effectFactory.CreateBrush();
            effectBrush.SetSourceParameter(EffectSource, surfaceBrush);
            effectBrush.Properties.InsertScalar(SaturationEffectPath, 0f);
        }

        private void CreateSpriteVisual()
        {
            spriteVisual = compositor.CreateSpriteVisual();
            spriteVisual.Brush = effectBrush;
            spriteVisual.Size = new Vector2(GetElementWidth(), GetElementHeight());
        }

        private void UpdateSpriteVisual()
        {
            spriteVisual.Brush = effectBrush;
            spriteVisual.Size = new Vector2(GetElementWidth(), GetElementHeight());
        }

        private void Insert()
        {
            visual.Children.InsertAtBottom(spriteVisual);
        }

        private float GetElementWidth()
        {
            if (frameworkElement.ActualWidth.Equals(double.NaN))
            {
                return 0;
            }
            return (float)frameworkElement.ActualWidth;
        }

        private float GetElementHeight()
        {
            if (frameworkElement.ActualHeight.Equals(double.NaN))
            {
                return 0;
            }
            return (float)frameworkElement.ActualHeight;
        }

        #region Level
        public double Level
        {
            get { return (double)GetValue(LevelProperty); }
            set { SetValue(LevelProperty, value); }
        }

        public static readonly DependencyProperty LevelProperty =
            DependencyProperty.Register(nameof(Level), typeof(double), typeof(Saturation), new PropertyMetadata(0.0, LevelChanged));

        private static void LevelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as Saturation).UpdateLevel();
        }

        private void UpdateLevel()
        {
            if (effectBrush != null)
            {
                var newvalue = (float)(Level / 100.0);
                effectBrush.Properties.InsertScalar(SaturationEffectPath, newvalue);
            }
        }
        #endregion

    }
}
