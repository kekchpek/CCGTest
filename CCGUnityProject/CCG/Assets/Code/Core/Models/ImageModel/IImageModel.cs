﻿using UnityAuxiliaryTools.Promises;
using UnityEngine;

namespace CCG.Core.Models.ImageModel
{
    public interface IImageModel
    {
        IPromise Initialize();
        Texture2D GetImage(string imageId);
        string[] GetAllImageIds();
    }
}