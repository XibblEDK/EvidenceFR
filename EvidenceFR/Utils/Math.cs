using Rage;

namespace EvidenceFR.Utils
{
    internal class Math
    {
        public static HitResult Raycast(Vector3 pos, Vector3 direction, float distance, TraceFlags traceFlags, params Entity[] toIgnore)
        {

            HitResult ray = World.TraceLine(pos, pos + direction * distance, traceFlags, toIgnore);

            return ray;
        }

        public static int Modulus(int x)
        {
            if (x > 0) return x;
            return x * -1;
        }

        public static float Modulus(float x)
        {
            if (x > 0) return x;
            return x * -1;
        }

    }
}
