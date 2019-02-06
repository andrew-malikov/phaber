namespace Phaber.Unsplash.Helpers {
    public static class Math {
        public static int GCD(this int a, int b) {
            if (b == 0) return a;
            return b.GCD(a % b);
        }
    }
}