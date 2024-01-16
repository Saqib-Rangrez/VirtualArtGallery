using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualArtGallery.dao;
using VirtualArtGallery.entity;

namespace VirtualArtGalleryTest
{
    [TestFixture]
    internal class GalleryServiceTests
    {
        private VirtualArtGalleryDao _VirtualArtGalleryDao;

        [SetUp]
        public void Setup()
        {
            AppDomain.CurrentDomain.SetData("APP_CONFIG_FILE", String.Format("{0}\\App.config", AppDomain.CurrentDomain.BaseDirectory));
            _VirtualArtGalleryDao = new VirtualArtGalleryDao();
        }

        [Test]
        public void AddGalleryTest()
        {
            Gallery gallery = new Gallery("New gallery Art", "Showcasing new artworks.", "123 Main Street, Cityville", 2, "Tue-Sun, 10am-6pm");
            bool res = _VirtualArtGalleryDao.AddGallery(gallery);
            Assert.IsTrue(res);
        }

        [Test]
        public void UpdateGalleryTest()
        {
            Gallery gallery = new Gallery("updated gallery Art", "Showcasing new artworks.", "Main Street, Cityville", 2, "Tue-Sun, 9am-5pm", 2011);
            bool res = _VirtualArtGalleryDao.UpdateGallery(gallery);
            Assert.IsTrue(res);
        }

        [Test]
        public void GetGalleryTest()
        {
            Gallery gallery = _VirtualArtGalleryDao.GetGalleryById(6);
            Assert.IsNotNull(gallery);
        }

        [Test]
        public void RemoveGalleryTest()
        {
            bool result = _VirtualArtGalleryDao.RemoveGallery(1);
            Assert.IsTrue(result);
        }
    }
}
