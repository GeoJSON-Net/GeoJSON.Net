using System.Collections.Generic;
using GeoJSON.Net.Geometry;
using Newtonsoft.Json;
using NUnit.Framework;

namespace GeoJSON.Net.Tests.Geometry
{
    [TestFixture]
    public class PolygonTests : TestBase
    {
        [Test]
        public void Can_Serialize()
        {
            var polygon = new Polygon(new List<LineString>
            {
                new LineString(new List<Position>
                {
                    new Position(52.379790828551016, 5.3173828125),
                    new Position(52.36721467920585, 5.456085205078125),
                    new Position(52.303440474272755, 5.386047363281249, 4.23),
                    new Position(52.379790828551016, 5.3173828125),
                })
            });

            var expectedJson = GetExpectedJson();
            var actualJson = JsonConvert.SerializeObject(polygon);

            JsonAssert.AreEqual(expectedJson, actualJson);
        }

        [Test]
        public void Can_Deserialize_With_Exterior_And_Inner_Rings()
        {
            var json = GetExpectedJson();

            var expectedPolygon = new Polygon(new List<LineString>
            {
                new LineString(new List<Position>
                {
                    new Position(34.9895035675793, -84.3228149414063),
                    new Position(35.2198194079344, -84.2912292480469),
                    new Position(35.2545909746502, -84.2404174804688),
                    new Position(35.2669256889501, -84.2253112792969),
                    new Position(35.2658044288675, -84.2074584960938),
                    new Position(35.24674063356, -84.19921875),
                    new Position(35.2411327816664, -84.1621398925781),
                    new Position(35.2489836657264, -84.1236877441406),
                    new Position(35.2489836657264, -84.0907287597656),
                    new Position(35.2646831532681, -84.0879821777344),
                    new Position(35.2770163313988, -84.0426635742188),
                    new Position(35.2915894845661, -84.0303039550781),
                    new Position(35.3061600145508, -84.0234375),
                    new Position(35.3274506849288, -84.0330505371094),
                    new Position(35.3431349602819, -84.0357971191406),
                    new Position(35.3487357494725, -84.0357971191406),
                    new Position(35.3554561839208, -84.0165710449219),
                    new Position(35.3733746083496, -84.0110778808594),
                    new Position(35.3912890552176, -84.0097045898437),
                    new Position(35.4147957290186, -84.0193176269531),
                    new Position(35.4293440441072, -84.0028381347656),
                    new Position(35.4740916077303, -83.9369201660156),
                    new Position(35.4763283326573, -83.9122009277344),
                    new Position(35.5042821432997, -83.8888549804688),
                    new Position(35.5165787389029, -83.8847351074219),
                    new Position(35.5210497612994, -83.8751220703125),
                    new Position(35.5210497612994, -83.8531494140625),
                    new Position(35.5210497612994, -83.8284301757813),
                    new Position(35.5344613341844, -83.8092041015625),
                    new Position(35.5411662799981, -83.8023376464844),
                    new Position(35.5623949105885, -83.7680053710937),
                    new Position(35.5623949105885, -83.7432861328125),
                    new Position(35.5623949105885, -83.7199401855469),
                    new Position(35.5690975207761, -83.6705017089844),
                    new Position(35.570214567966, -83.6334228515625),
                    new Position(35.5769165240386, -83.6100769042969),
                    new Position(35.5746826009809, -83.5963439941406),
                    new Position(35.559043395259, -83.5894775390625),
                    new Position(35.5657462857628, -83.5523986816406),
                    new Position(35.5635120512197, -83.4974670410156),
                    new Position(35.5869684067865, -83.4700012207031),
                    new Position(35.6081849043775, -83.4466552734375),
                    new Position(35.6360927786314, -83.3793640136719),
                    new Position(35.6561804163202, -83.3573913574219),
                    new Position(35.6662223410348, -83.3230590820312),
                    new Position(35.6539487059976, -83.3148193359375),
                    new Position(35.6606436498816, -83.2997131347656),
                    new Position(35.6718006423877, -83.2859802246094),
                    new Position(35.6907639509368, -83.2612609863281),
                    new Position(35.699686301252, -83.2571411132813),
                    new Position(35.7152980121253, -83.2557678222656),
                    new Position(35.7231027209226, -83.2351684570313),
                    new Position(35.727562211272, -83.1980895996094),
                    new Position(35.7531994355703, -83.1623840332031),
                    new Position(35.763229145499, -83.1582641601563),
                    new Position(35.7699149163548, -83.1033325195313),
                    new Position(35.7843988251953, -83.0868530273438),
                    new Position(35.7877408909866, -83.0511474609375),
                    new Position(35.7832847720374, -83.0168151855469),
                    new Position(35.7788284032737, -83.001708984375),
                    new Position(35.7933106883517, -82.9673767089844),
                    new Position(35.820040281161, -82.9454040527344),
                    new Position(35.8512134345006, -82.9193115234375),
                    new Position(35.869021165017, -82.9083251953125),
                    new Position(35.8779235299512, -82.9055786132813),
                    new Position(35.9235324471824, -82.9124450683594),
                    new Position(35.9468829321814, -82.8836059570313),
                    new Position(35.9513298615227, -82.8561401367188),
                    new Position(35.9424357525543, -82.8424072265625),
                    new Position(35.924644531441, -82.825927734375),
                    new Position(35.9279806903827, -82.8067016601563),
                    new Position(35.9424357525543, -82.8053283691406),
                    new Position(35.9735607534962, -82.7792358398438),
                    new Position(35.9924520905583, -82.7806091308594),
                    new Position(36.0035625289507, -82.7613830566406),
                    new Position(36.0446575392153, -82.6954650878906),
                    new Position(36.0602014123929, -82.6446533203125),
                    new Position(36.0602014123929, -82.6130676269531),
                    new Position(36.0335528934004, -82.606201171875),
                    new Position(35.9913409606354, -82.606201171875),
                    new Position(35.979117498575, -82.606201171875),
                    new Position(35.9613345373669, -82.5787353515625),
                    new Position(35.9513298615227, -82.5677490234375),
                    new Position(35.9724493575368, -82.5306701660156),
                    new Position(36.0068953552447, -82.4647521972656),
                    new Position(36.0701922812085, -82.4166870117188),
                    new Position(36.1012668692145, -82.3796081542969),
                    new Position(36.1179089165637, -82.3548889160156),
                    new Position(36.1134713820522, -82.3411560058594),
                    new Position(36.1334383124587, -82.2958374023438),
                    new Position(36.1356565467854, -82.2628784179687),
                    new Position(36.1356565467854, -82.2340393066406),
                    new Position(36.154509006695, -82.2216796875),
                    new Position(36.1556178338186, -82.2038269042969),
                    new Position(36.1445288570277, -82.1900939941406),
                    new Position(36.1500735414076, -82.1543884277344),
                    new Position(36.1345474374601, -82.1406555175781),
                    new Position(36.116799556445, -82.1337890625),
                    new Position(36.1057050932792, -82.1214294433594),
                    new Position(36.1079241112865, -82.08984375),
                    new Position(36.1267832332643, -82.0527648925781),
                    new Position(36.1290016556965, -82.0362854003906),
                    new Position(36.2940976837303, -81.9126892089844),
                    new Position(36.3095921540914, -81.8907165527344),
                    new Position(36.3350406720961, -81.8632507324219),
                    new Position(36.344996525619, -81.8302917480469),
                    new Position(36.3560570924018, -81.8014526367188),
                    new Position(36.3461026530064, -81.7794799804687),
                    new Position(36.3383594313405, -81.7616271972656),
                    new Position(36.3383594313405, -81.7369079589844),
                    new Position(36.3383594313405, -81.7190551757813),
                    new Position(36.3350406720961, -81.7066955566406),
                    new Position(36.3427842237072, -81.7066955566406),
                    new Position(36.3571630626544, -81.7231750488281),
                    new Position(36.379279167408, -81.7327880859375),
                    new Position(36.4002836433235, -81.7369079589844),
                    new Position(36.4135467039288, -81.7369079589844),
                    new Position(36.4234925134723, -81.7245483398437),
                    new Position(36.4455897517792, -81.7176818847656),
                    new Position(36.4754110428296, -81.6984558105469),
                    new Position(36.5107399414667, -81.6984558105469),
                    new Position(36.5306053641136, -81.705322265625),
                    new Position(36.55929085774, -81.6915893554688),
                    new Position(36.5648060784035, -81.6806030273438),
                    new Position(36.5868630234418, -81.6819763183594),
                    new Position(36.5637030657692, -81.0420227050781),
                    new Position(36.5614969932526, -80.7426452636719),
                    new Position(36.540536162629, -79.8912048339844),
                    new Position(36.5394328035512, -78.68408203125),
                    new Position(36.540536162629, -77.8834533691406),
                    new Position(36.5416395059613, -76.9166564941406),
                    new Position(36.5504656857595, -76.9166564941406),
                    new Position(36.551568887374, -76.31103515625),
                    new Position(36.5493624683978, -75.7960510253906),
                    new Position(36.075742215627, -75.6298828125),
                    new Position(35.8222673411451, -75.4925537109375),
                    new Position(35.6394410689739, -75.3936767578125),
                    new Position(35.4382955473967, -75.41015625),
                    new Position(35.2635618621521, -75.43212890625),
                    new Position(35.187277675989, -75.487060546875),
                    new Position(35.1738083179996, -75.5914306640625),
                    new Position(35.0479867342673, -75.9210205078125),
                    new Position(34.8679049625687, -76.17919921875),
                    new Position(34.6286879737706, -76.4154052734375),
                    new Position(34.5744295186527, -76.4593505859375),
                    new Position(34.5337124213957, -76.53076171875),
                    new Position(34.5518113691705, -76.5911865234375),
                    new Position(34.6151266834622, -76.651611328125),
                    new Position(34.6332079113796, -76.761474609375),
                    new Position(34.5970415161442, -77.069091796875),
                    new Position(34.4567480034781, -77.376708984375),
                    new Position(34.3207552752374, -77.5909423828125),
                    new Position(33.9798087287246, -77.8326416015625),
                    new Position(33.8019735180659, -77.9150390625),
                    new Position(33.7380448632891, -77.9754638671875),
                    new Position(33.8521697014074, -78.11279296875),
                    new Position(33.8521697014074, -78.2830810546875),
                    new Position(33.8156663087028, -78.4808349609375),
                    new Position(34.8047829195724, -79.6728515625),
                    new Position(34.8363499907639, -80.782470703125),
                    new Position(34.9174668892825, -80.782470703125),
                    new Position(35.0929453137326, -80.9307861328125),
                    new Position(35.0299963690257, -81.0516357421875),
                    new Position(35.0524837066247, -81.0516357421875),
                    new Position(35.1378791196342, -81.0516357421875),
                    new Position(35.1962560078637, -82.3150634765625),
                    new Position(35.1962560078637, -82.3590087890625),
                    new Position(35.2231850497018, -82.4029541015625),
                    new Position(35.1693180360113, -82.4688720703125),
                    new Position(35.1154153142536, -82.6885986328125),
                    new Position(35.0614769084972, -82.781982421875),
                    new Position(35.0030033952767, -83.1060791015625),
                    new Position(34.9985037001463, -83.616943359375),
                    new Position(34.9850031301711, -84.056396484375),
                    new Position(34.9850031301711, -84.22119140625),
                    new Position(34.9895035675793, -84.3228149414063),
                }),
                new LineString(new List<Position>
                {
                    new Position(35.7420538306804, -75.6903076171875),
                    new Position(35.7420538306804, -75.5914306640625),
                    new Position(35.5858515932324, -75.5419921875),
                    new Position(35.3263302630748, -75.56396484375),
                    new Position(35.2859847360657, -75.6903076171875),
                    new Position(35.1648275060503, -75.970458984375),
                    new Position(34.9940037575758, -76.2066650390625),
                    new Position(35.0299963690257, -76.300048828125),
                    new Position(35.0794603404798, -76.409912109375),
                    new Position(35.1064280573642, -76.5252685546875),
                    new Position(35.2590765425257, -76.4208984375),
                    new Position(35.2949521474066, -76.3385009765625),
                    new Position(35.2994354805454, -76.0858154296875),
                    new Position(35.4427709258577, -75.948486328125),
                    new Position(35.536696378395, -75.8660888671875),
                    new Position(35.5679804580121, -75.772705078125),
                    new Position(35.6349766506773, -75.706787109375),
                    new Position(35.7420538306804, -75.706787109375),
                    new Position(35.7420538306804, -75.6903076171875),
                })
            });

            var actualPolygon = JsonConvert.DeserializeObject<Polygon>(json);
            Assert.AreEqual(expectedPolygon, actualPolygon);
        }

        [Test]
        public void Can_Deserialize()
        {
            var json = GetExpectedJson();

            var expectedPolygon = new Polygon(new List<LineString>
            {
                new LineString(new List<Position>
                {
                    new Position(52.379790828551016, 5.3173828125),
                    new Position(52.36721467920585, 5.456085205078125),
                    new Position(52.303440474272755, 5.386047363281249, 4.23),
                    new Position(52.379790828551016, 5.3173828125),
                })
            });

            var actualPolygon = JsonConvert.DeserializeObject<Polygon>(json);

            Assert.AreEqual(expectedPolygon, actualPolygon);
        }
        
        [Test]
        public void Equals_GetHashCode_Contract()
        {
            var rnd = new System.Random();
            var offset = rnd.NextDouble() * 60;
            if (rnd.NextDouble() < 0.5)
            {
                offset *= -1;
            }

            var left = GetPolygon(offset);
            var right = GetPolygon(offset);

            Assert.AreEqual(left, right);
            Assert.AreEqual(right, left);

            Assert.IsTrue(left.Equals(right));
            Assert.IsTrue(left.Equals(left));
            Assert.IsTrue(right.Equals(left));
            Assert.IsTrue(right.Equals(right));

            Assert.IsTrue(left == right);
            Assert.IsTrue(right == left);

            Assert.AreEqual(left.GetHashCode(), right.GetHashCode());
        }

        
        private Polygon GetPolygon(double offset = 0.0)
        {
            var polygon = new Polygon(new List<LineString>
            {
                new LineString(new List<Position>
                {
                    new Position(52.379790828551016 + offset, 5.3173828125 + offset),
                    new Position(52.36721467920585 + offset, 5.456085205078125 + offset),
                    new Position(52.303440474272755 + offset, 5.386047363281249 + offset, 4.23 + offset),
                    new Position(52.379790828551016 + offset, 5.3173828125 + offset),
                })
            });
            return polygon;
        }
    }
}