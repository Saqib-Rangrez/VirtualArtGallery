using System;
using System.Data;
using System.Data.SqlClient;
using NUnit.Framework;
using VirtualArtGallery.entity;
using VirtualArtGallery.dao;


namespace VirtualArtGalleryUnitTest
{
    [TestFixture]
    internal class ArtworkServiceTests
    {
        private VirtualArtGalleryDao _VirtualArtGalleryDao;

        [SetUp]
        public void Setup()
        {
            AppDomain.CurrentDomain.SetData("APP_CONFIG_FILE", String.Format("{0}\\App.config", AppDomain.CurrentDomain.BaseDirectory));
            _VirtualArtGalleryDao = new VirtualArtGalleryDao();
        }

        [Test]
        public void AddArtworkTest()
        {
            Artwork artwork = new Artwork("Landscape", "A beautiful landscape painting", Convert.ToDateTime("2024-01-10"), "Watercolor on Paper", "landscape.jpg", 3);
            bool result = _VirtualArtGalleryDao.AddArtwork(artwork);
            Assert.IsTrue(result);
        }

        [Test]
        public void UpdateArtworkTest()
        {

            Artwork artwork = new Artwork("New Artwork", "New artwork updated by me", Convert.ToDateTime("2024-01-11"), "paint on canvas", "portrait.png", 2, 13);
            bool result = _VirtualArtGalleryDao.UpdateArtwork(artwork);
            Assert.IsTrue(result);
        }

        [Test]
        public void GetArtworkById()
        {
            Artwork artwork = _VirtualArtGalleryDao.GetArtworkByID(2);
            Assert.IsNotNull(artwork);

        }

        [Test]
        public void RemoveArtworkTest()
        {
            bool result = _VirtualArtGalleryDao.RemoveArtwork(12);
            Assert.IsTrue(result);
        }        
    }
}