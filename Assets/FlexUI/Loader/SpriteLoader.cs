//#define USE_RESOURCE
#define USE_SPRITE_REF


using UnityEngine;

namespace FlexUI
{

    /// <summary>
    /// Note!!!!
    /// Fix the TODO following!
    /// </summary>
    public class SpriteLoader
    {
        //TODO: make a list references all sprites that runtime may used. Those
        //spripe should be packed , not located in Resource folder.
        //And then, the loader load sprite from that list, not from Resource folder to 
        // decrease drawcall.
        public static Sprite Load(string path, bool spriteInSheet = false)
        {
            #if USE_RESOURCE
            return SpriteLoadingHelper.Load(path, spriteInSheet);
            #endif 

            #if USE_SPRITE_REF
            return SpriteReferences.GetSpriteByName(path);
            #endif

        }
    }

}
