using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EllaBookMaker.Model
{
    public enum ParticleType
    {
        火焰效果 = 1,
        烟花效果 = 2,
        太阳效果 = 3,
        星系效果 = 4,
        花束效果 = 5,
        流星效果 = 6,
        漩涡效果 = 7,
        爆炸效果 = 8,
        烟雾效果 = 9,
        下雪效果 = 10,
        下雨效果 = 11
    }
    //[Serializable]
    //class Particle:Sprite
    //{
    //    public ParticleType particleType=ParticleType.下雪效果;
    //    public AnimationCategory category=AnimationCategory.Auto;
    //    public Particle(Sprite baseSprite)
    //        : base()
    //    {
    //        InitParticle(baseSprite);
    //    }
    //    private void InitParticle(Sprite baseSprite) 
    //    {
    //        this.ImgName = baseSprite.ImgName;
    //        this.Height = baseSprite.Height;
    //        this.ParentPage = baseSprite.ParentPage;
    //        this.SoundName = baseSprite.SoundName;
    //        this.SoundSourcePath = baseSprite.SoundSourcePath;
    //        this.buddyMovie = baseSprite.buddyMovie;
    //        this.SourceImagePath = baseSprite.SourceImagePath;
    //        this.TopLeftPointProperty = baseSprite.TopLeftPointProperty;
    //        this.Angle = baseSprite.Angle;
    //        this.Opacity = baseSprite.Opacity;
    //        this.Scale = baseSprite.Scale;
    //        this.Width = baseSprite.Width;
    //        this.CorePoint = baseSprite.CorePoint;
    //        this.X = baseSprite.X;
    //        this.Y = baseSprite.Y;
    //        this.Z = baseSprite.Z;
    //        this.Touchenable = baseSprite.Touchenable;
    //        this.touchableEndPoint = baseSprite.touchableEndPoint;
    //        this.ParentPage = baseSprite.ParentPage;
    //        this.Initialize();
    //    }
    //}
}
