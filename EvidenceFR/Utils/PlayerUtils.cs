using EvidenceFR.Functions.Object;
using Rage;
using Rage.Native;
using RAGENativeUI.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EvidenceFR.Utils
{
    internal class PlayerUtils
    {

        public void TakePhoto(EvidenceEntity? evidenceEntity = null)
        {
            NativeFunction.Natives.CELL_CAM_ACTIVATE(false, false);

            NativeFunction.Natives.CREATE_MOBILE_PHONE(3);

            GameFiber.StartNew(delegate
            {

                // TODO: Deactivate cellphone inputs
                // Store images
                // Checker whether requested entity is visible on images


                var run = true;
                var camActive = false;

                Scaleform sf = new Scaleform();
                sf.Load("DIGITAL_CAMERA");
                sf.CallFunction("SHOW_PHOTO_FRAME", true);
                sf.CallFunction("SET_REMAINING_PHOTOS", 23, 1050);
                sf.CallFunction("SHOW_REMAINING_PHOTOS", true);
                sf.CallFunction("SHOW_FOCUS_LOCK", true, "LSPD Property");
                while (run)
                {
                    GameFiber.Sleep(1);
                    Game.DisplaySubtitle("" + camActive);
                    if (camActive) NativeFunction.Natives.DRAW_SCALEFORM_MOVIE_FULLSCREEN(sf.Handle, 255, 255, 255, 255, 0);
                    if (Game.IsKeyDown(Keys.Enter))
                    {
                        if (!camActive)
                        {
                            sf.CallFunction("OPEN_SHUTTER", 0);
                            Game.DisplayHelp("activating");
                            NativeFunction.Natives.CELL_CAM_ACTIVATE(true, true);
                            camActive = true;
                            GameFiber.Sleep(100);
                        }
                        else
                        {
                            Game.DisplayHelp("deactivating");
                            NativeFunction.Natives.CELL_CAM_ACTIVATE(false, false);
                            sf.CallFunction("CLOSE_SHUTTER");
                            camActive = false;
                            GameFiber.Sleep(250);
                        }
                    }
                }
                GameFiber.SleepUntil(() => Game.IsKeyDown(Keys.Space), 100);
                NativeFunction.Natives.CELL_CAM_ACTIVATE(false, false);
                run = false;
                if (sf.IsLoaded) NativeFunction.Natives.SET_SCALEFORM_MOVIE_AS_NO_LONGER_NEEDED("DIGITAL_CAMERA");
            });

        }




    }
}
