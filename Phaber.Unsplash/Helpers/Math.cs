namespace Phaber.Unsplash.Helpers {
    public static class Math {
        public static int Gcd(this int a, int b) {
            if (b == 0) return a;
            return b.Gcd(a % b);
        }
    }
}