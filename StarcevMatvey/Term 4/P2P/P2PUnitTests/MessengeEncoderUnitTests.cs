using NUnit.Framework;
using P2P.MessengeEncoder;
using P2P.MessengeTypes;
using System;
using System.Linq;
using System.Net;

namespace P2PUnitTests
{
    public class MessengeEncoderUnitTests
    {
        [Test]
        public void ToMessengeTest()
        {
            var encoder = new MessengeEncoder();
            var mes = new Messenge("\x22\x44\x66", Union.Union, TypeOfData.RegularMessenge);

            var outp = encoder.ToMessenge(mes);
            var pat = new byte[outp.Length];
            pat[0] = (byte)0x01;
            pat[1] = (byte)0x01;
            pat[2] = (byte)0x22;
            pat[3] = (byte)0x44;
            pat[4] = (byte)0x66;

            Assert.AreEqual(outp, pat);
        }

        [Test]
        public void FromMessengeTest()
        {
            var size = 4;

            var encoder = new MessengeEncoder(size);
            var mes = new byte[size];
            mes[0] = (byte)0x01;
            mes[1] = (byte)0x01;
            mes[2] = (byte)0x22;
            mes[3] = (byte)0x44;

            var outp = encoder.FromMessenge(mes);
            var pat = new Messenge("\x22\x44", Union.Union, TypeOfData.RegularMessenge);

            Assert.AreEqual(outp.Data, pat.Data);
            Assert.AreEqual(outp.Union, pat.Union);
            Assert.AreEqual(outp.Type, pat.Type);
        }

        [Test]
        public void GetPortTest()
        {
            var encoder = new MessengeEncoder();

            var fMes = new Messenge($"111", Union.Union, TypeOfData.RegularMessenge);

            Assert.Throws<Exception>(() => encoder.GetPort(fMes));

            var sMes = fMes.WithType(TypeOfData.Listeners);

            Assert.AreEqual(111, encoder.GetPort(sMes));
        }

        [Test]
        public void GetConnections()
        {
            var encoder = new MessengeEncoder();

            var ePoint = IPEndPoint.Parse("1111");
            var fMes = new Messenge(ePoint.ToString(), Union.Union, TypeOfData.RegularMessenge);

            Assert.Throws<Exception>(() => encoder.GetConnections(fMes));

            var sMes = fMes.WithType(TypeOfData.Listeners);

            Assert.AreEqual(encoder.GetConnections(sMes).First().Port, ePoint.Port);
        }
    }
}
