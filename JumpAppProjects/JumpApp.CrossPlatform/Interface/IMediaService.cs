using System;
using System.Collections.Generic;
using System.Text;

namespace JumpApp.Interface
{
    public interface IMediaService
    {
        byte[] ResizeImage(byte[] imageData, float width, float height);
    }
}
