﻿using Rage;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvidenceFR.Utils
{
    internal class TextureRendererManager
    {
        private static string basePath = @"C:\Program Files\Epic Games\GTAV\plugins\LSPDFR\EvidenceFR\";

        public static Texture headen;
        public static Texture headdis;

        public static Texture bodyen;
        public static Texture bodydis;

        public static Texture leftarmen;
        public static Texture leftarmdis;

        public static Texture rightarmen;
        public static Texture rightarmdis;

        public static Texture leftlegen;
        public static Texture leftlegdis;

        public static Texture rightlegen;
        public static Texture rightlegdis;

        public static List<Texture> RenderedTextures = new List<Texture>();

        public static bool canUseUI = true;

        private static Dictionary<string, Texture> TexturePaths = new Dictionary<string, Texture>()
        {
            {"headen.png",headen},
            {"headdis.png",headdis},

            {"bodyen.png",bodyen},
            {"bodydis.png",bodydis},

            {"leftarmen.png",leftarmen},
            {"leftarmdis.png",leftarmdis},

            {"rightarmen.png",rightarmen},
            {"rightarmdis.png",rightarmdis},

            {"leftlegen.png",leftlegen},
            {"leftlegdis.png",leftlegdis},

            {"rightlegen.png",rightlegen},
            {"rightlegdis.png",rightlegdis},
        };
        public static void InitializeTextures()
        {

            headen = Game.CreateTextureFromFile(@"C:\Program Files\Epic Games\GTAV\plugins\LSPDFR\EvidenceFR\headen.png");
            headdis = Game.CreateTextureFromFile(@"C:\Program Files\Epic Games\GTAV\plugins\LSPDFR\EvidenceFR\headdis.png");

            bodyen = Game.CreateTextureFromFile(@"C:\Program Files\Epic Games\GTAV\plugins\LSPDFR\EvidenceFR\bodyen.png");
            bodydis = Game.CreateTextureFromFile(@"C:\Program Files\Epic Games\GTAV\plugins\LSPDFR\EvidenceFR\bodydis.png");

            leftarmen = Game.CreateTextureFromFile(@"C:\Program Files\Epic Games\GTAV\plugins\LSPDFR\EvidenceFR\leftarmen.png");
            leftarmdis = Game.CreateTextureFromFile(@"C:\Program Files\Epic Games\GTAV\plugins\LSPDFR\EvidenceFR\leftarmdis.png");

            rightarmen = Game.CreateTextureFromFile(@"C:\Program Files\Epic Games\GTAV\plugins\LSPDFR\EvidenceFR\rightarmen.png");
            rightarmdis = Game.CreateTextureFromFile(@"C:\Program Files\Epic Games\GTAV\plugins\LSPDFR\EvidenceFR\rightarmdis.png");

            leftlegen = Game.CreateTextureFromFile(@"C:\Program Files\Epic Games\GTAV\plugins\LSPDFR\EvidenceFR\leftlegen.png");
            leftlegdis = Game.CreateTextureFromFile(@"C:\Program Files\Epic Games\GTAV\plugins\LSPDFR\EvidenceFR\leftlegdis.png");

            rightlegen = Game.CreateTextureFromFile(@"C:\Program Files\Epic Games\GTAV\plugins\LSPDFR\EvidenceFR\rightlegen.png");
            rightlegdis = Game.CreateTextureFromFile(@"C:\Program Files\Epic Games\GTAV\plugins\LSPDFR\EvidenceFR\rightlegdis.png");

            s_subscribeRenderer();
        }

        private static void s_subscribeRenderer()
        {
            Game.RawFrameRender += s_renderTextures;
        }

        public static void UnsubscribeRenderer()
        {
            Game.RawFrameRender -= s_renderTextures;
        }

        private static void s_renderTextures(object s, GraphicsEventArgs e)
        {
            //e.Graphics.DrawTexture(bodydis, new RectangleF(Game.Resolution.Width / 2 - bodydis.Size.Width / 2, Game.Resolution.Height / 2 - bodydis.Size.Height / 2, bodydis.Size.Width, bodydis.Size.Height));

            try
            {
                foreach (Texture txt in RenderedTextures)
                {
                    if (txt != null)
                    {
                        e.Graphics.DrawTexture(txt, Game.Resolution.Width/2 - txt.Size.Width/2/2, Game.Resolution.Height/ 2 - txt.Size.Height/2/2, txt.Size.Width/2, txt.Size.Height/2);
                    } else
                    {
                        Logging.Log(Logging.LogLevel.Warning, "Texture is null: " + TexturePaths.FirstOrDefault(x => x.Value == txt).Key.ToString());
                    }
                }

            } catch(Exception ex)
            {
                Game.LogTrivial(ex.ToString());
            }
        }

    }
}
