using Moq;
using System;
using Xunit;


namespace TestProject2
{
    public class UnitTest1
    {
        [Fact]
        public void RoomGetAllResultCorrectTest()
        {
            var mock = new Mock<IRoomService>();
            mock.Setup(a => a.Get(1)).Returns(new RoomDTO());
            IMapper mapper = new MapperConfiguration(
               cfg => cfg.CreateMap<RoomDTO, RoomModel>()
               ).CreateMapper();
            var expected = mapper.Map<RoomDTO, RoomModel>(mock.Object.Get(1));

            RoomController controller = new RoomController(mock.Object);
            var result = controller.Get(new HttpRequestMessage(), 1);

            Assert.AreEqual(result.Id, "1");
        }
    }
}
