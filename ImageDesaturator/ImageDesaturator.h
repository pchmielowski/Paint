// ImageDesaturator.h
#pragma once
#using <System.Drawing.dll>
#include <iostream>
using namespace std;

using namespace System::Drawing::Drawing2D;
using namespace System::Drawing::Imaging;
using namespace System::Drawing;

namespace ImageDesaturator {

  public ref class Class1
  {
  public:
    void doDesaturate(Bitmap^ bitmap)
    {
      Rectangle bRect(Point(0, 0), bitmap->Size);
      BitmapData^ bData = bitmap->LockBits(bRect, ImageLockMode::ReadWrite, bitmap->PixelFormat);

      int height = bitmap->Size.Height;
      int width = bitmap->Size.Width;
      int pixelSize = bData->Stride / bData->Width;

      for (int x = 0; x < width; x++)
      {
        for (int y = 0; y < height; y++)
        {
          unsigned char* pixelBaseAddress =
            reinterpret_cast<unsigned char*>((bData->Scan0).ToPointer()) + (y * bData->Stride) + (x * pixelSize);
          unsigned char value = 0;
          const int NUM_CHANNELS = 3;
          for (int channelIdx = 0; channelIdx < NUM_CHANNELS; ++channelIdx)
          {
            value += (unsigned char)(*pixelBaseAddress++ / NUM_CHANNELS);
          }
          pixelBaseAddress =
            reinterpret_cast<unsigned char*>((bData->Scan0).ToPointer()) + (y * bData->Stride) + (x * pixelSize);

          for (int channelIdx = 0; channelIdx < NUM_CHANNELS; ++channelIdx)
          {
            *pixelBaseAddress++ = value;
          }
        }
      }
      bitmap->UnlockBits(bData);
    }

    void desaturate(unsigned char NUM_CHANNELS, unsigned char* pixelBaseAddress, unsigned char value)
    {
      for (int channelIdx = 0; channelIdx < NUM_CHANNELS; ++channelIdx)
      {
        *pixelBaseAddress++ = value;
      }
    }
  };
}
