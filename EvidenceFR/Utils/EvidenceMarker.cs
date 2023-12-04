using Rage;
using Rage.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvidenceFR.Utils
{
    public class EvidenceMarker
    {
        public int markerType = 0;
        public float scaleX = 0.5f;
        public float scaleY = 0.5f;
        public float scaleZ = 0.5f;
        public int R = 255;
        public int G = 0;
        public int B = 0;
        public int Alpha = 100;
        public bool BobUpAndDown = true;
        public bool FaceCamera = false;
        public bool RotateContinuously = false;


        public EvidenceMarker(int markerType = 0, float scaleX = 0.5f, float scaleY = 0.5f, float scaleZ = 0.5f, int R=255, int G=0, int B=0, int Alpha = 100, bool BobUpAndDown = true, bool FaceCamera = false, bool RotateContinuously = false)
        {
            this.markerType = markerType;
            this.scaleX = scaleX;
            this.scaleY = scaleY; 
            this.scaleZ = scaleZ;
            this.R = R;
            this.G = G;
            this.B = B;
            this.Alpha = Alpha;
            this.BobUpAndDown = BobUpAndDown;
            this.FaceCamera = FaceCamera;
            this.RotateContinuously = RotateContinuously;
        }

        public void Draw(Vector3 position)
        {
            NativeFunction.Natives.DRAW_​MARKER(markerType, position, 0,0,0, 0,0,0, scaleX, scaleY, scaleZ, R, G, B, Alpha, BobUpAndDown, FaceCamera, 0, RotateContinuously, 0, 0, false);
        }

    }
}
