using CarouselView.FormsPlugin.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace JumpApp.Controls
{
    public class AutoscrollCarouselBehavior : Behavior<CarouselView.FormsPlugin.Abstractions.CarouselViewControl>
    {


        /// <summary>
        /// Scroll delay in milliseconds
        /// </summary>
        public int Delay { get; set; } = 3000;

        private bool runTimer;
        private CarouselViewControl attachedCarousel;

        protected override void OnAttachedTo(CarouselViewControl bindable)
        {
            base.OnAttachedTo(bindable);
            runTimer = true;
            attachedCarousel = bindable;

            Device.StartTimer(TimeSpan.FromMilliseconds(Delay), () =>
            {
                MoveCarousel();
                return runTimer;
            });

        }

        protected override void OnDetachingFrom(CarouselViewControl bindable)
        {
            runTimer = false;
            base.OnDetachingFrom(bindable);
        }

        void MoveCarousel()
        {
            if (attachedCarousel.ItemsSource != null)
            {
                if (attachedCarousel.Position < attachedCarousel.ItemsSource.GetCount() - 1)
                {
                    attachedCarousel.Position++;
                }
                else
                {
                    attachedCarousel.Position = 0;
                }
            }
        }
    }
}
