using System;
using System.IO;

namespace PdfTemplate.iTextSharp.LGPLv2.Zip
{
    // https://stackoverflow.com/questions/16585488/writing-to-ziparchive-using-the-httpcontext-outputstream

    public class ZipWrapStream : Stream
    {
        private readonly Stream stream;

        private long position = 0;

        public ZipWrapStream(Stream stream)
        {
            this.stream = stream;
        }

        public override bool CanSeek { get { return false; } }

        public override bool CanWrite { get { return true; } }

        public override long Position
        {
            get { return position; }
            set { throw new NotImplementedException(); }
        }

        public override bool CanRead { get { throw new NotImplementedException(); } }

        public override long Length { get { throw new NotImplementedException(); } }

        public override void Flush()
        {
            stream.Flush();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            throw new NotImplementedException();
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotImplementedException();
        }

        public override void SetLength(long value)
        {
            throw new NotImplementedException();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            position += count;
            stream.Write(buffer, offset, count);
        }
    }
}
