using System;
using Phaber.Unsplash.Helpers;

namespace Phaber.Unsplash.Entities {
    /// <summary>
    /// Represent a ratio between width and height
    /// </summary>
    public class AspectRatio {
        public readonly int Width;
        public readonly int Height;

        private readonly Lazy<double> _coefficient;
        public double Coefficient => _coefficient.Value;

        public AspectRatio(int width, int height) {
            if (width < 0 || height < 0)
                throw new ArgumentException();

            var gcd = width.Gcd(height);

            Width = width / gcd;
            Height = height / gcd;

            _coefficient = new Lazy<double>(() => Width / Height);
        }

        public AspectRatio(Photo photo) : this(photo.Width, photo.Height) { }
    }
}