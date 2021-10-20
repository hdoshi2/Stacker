using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Newtonsoft.Json;
using RestSharp;
using Stacker.GeoJsonClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using View = Autodesk.Revit.DB.View;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Stacker.Commands
{
    public partial class GeoZoningForm : System.Windows.Forms.Form
    {
        public object QueryResultRegrid { get; set; }
        public string JsonRegrid { get; set; }
        public object QueryResultZoneomics { get; set; }
        public string JsonZoneomics { get; set; }
        private bool _winformExpanded { get; set; }

        private Document _doc;
        private UIDocument _uidoc;
        
        public double BoundingBoxArea { get; set; }
        public double BoundingBoxWidth { get; set; }
        public double BoundingBoxHeight { get; set; }

        public GeoZoningForm(Document doc, UIDocument uidoc)
        {
            InitializeComponent();

            _doc = doc;
            _uidoc = uidoc;

            gbJSONresults.Hide();
            this.Width = Convert.ToInt32(this.Width / 3);
            _winformExpanded = false;
        }

        private void btnAPICall_Click(object sender, EventArgs e)
        {
            string addressLine1 = tbStreedAddress1.Text;
            string addressLine2 = tbStreedAddress2.Text;
            string city = cbCity.Text;
            string state = cbState.Text;
            string zip = tbZipCode.Text;
            string country = tbCountry.Text;

            string fullAddress = $"{addressLine1}, {city}, {state} {zip}, {country}";
            if (addressLine2 != "")
                fullAddress = $"{addressLine1}, {addressLine2},{city}, {state} {zip}, {country}";


            if (cbRegrid.Checked)
            {
                var clietRegrid = new RestClient("https://app.regrid.com/api/v1/search.json?");
                var requestRegrid = new RestRequest(Method.GET);

                requestRegrid.AddParameter("query", fullAddress);
                requestRegrid.AddParameter("strict", "1");
                requestRegrid.AddParameter("limit", "1");
                requestRegrid.AddParameter("token", "ay49YmoCTj_sV_p4MRpqnF9wwPKPRxpzSSK-EbGaMwpKipxZYy43oQoseSFMXECy");

                requestRegrid.AddHeader("content-type", "application/json");
                QueryResultRegrid = clietRegrid.Execute<Object>(requestRegrid).Data;
                JsonRegrid = JsonConvert.SerializeObject(QueryResultRegrid, Formatting.Indented);

                string directory = $"C:\\Users\\hdosh\\Desktop\\JSON_Data\\Regrid";
                if (Directory.Exists(directory))
                {
                    string path = $"{directory}\\JSON_Regrid_{fullAddress}.json";
                    System.IO.File.WriteAllText(@path, JsonRegrid);
                }

                if (JsonRegrid != null && JsonRegrid.Length > 30)
                {
                    lblRegridStatus.Visible = true;
                    lblRegridStatus.ForeColor = System.Drawing.Color.DarkGreen;
                    lblRegridStatus.Text = "Successful";

                    tbRegrid.Text = JsonRegrid;
                }
                else
                {
                    lblRegridStatus.Visible = true;
                    lblRegridStatus.ForeColor = System.Drawing.Color.Red;
                    lblRegridStatus.Text = "Failed";
                }
            }

            if (cbZoneomics.Checked)
            {
                var clientZoneomics = new RestClient("https://www.zoneomics.com/api/get_zone_details");
                var requestZoneomics = new RestRequest(Method.GET);

                requestZoneomics.AddParameter("api_key", "a9454176416681b8051d7ee5479d3e85c2afd650");
                requestZoneomics.AddParameter("address", fullAddress);
                requestZoneomics.AddParameter("output_fields", "all");

                requestZoneomics.AddHeader("content-type", "application/json");
                QueryResultZoneomics = clientZoneomics.Execute<Object>(requestZoneomics).Data;
                JsonZoneomics = JsonConvert.SerializeObject(QueryResultZoneomics, Formatting.Indented);



                string directory = $"C:\\Users\\hdosh\\Desktop\\JSON_Data\\Zoneomics";
                if (Directory.Exists(directory))
                {
                    string path = $"{directory}\\JSON_Zoneomics_{fullAddress}.json";
                    System.IO.File.WriteAllText(@path, JsonZoneomics);
                }



                if (JsonZoneomics != null && JsonZoneomics.Length > 30)
                {
                    lblZoneomicsStatus.Visible = true;
                    lblZoneomicsStatus.ForeColor = System.Drawing.Color.DarkGreen;
                    lblZoneomicsStatus.Text = "Successful";

                    tbZoneonics.Text = JsonZoneomics;
                }
                else
                {
                    lblZoneomicsStatus.Visible = true;
                    lblZoneomicsStatus.ForeColor = System.Drawing.Color.Red;
                    lblZoneomicsStatus.Text = "Failed";
                }

                var zoneomicsJObject = JObject.Parse(JsonZoneomics);
                var dictZoneomicsJObject = zoneomicsJObject["data"]
                            .Children().Cast<JProperty>()
                            .ToDictionary(x => x.Name, x => (string)x.Value);


                if (dictZoneomicsJObject.ContainsKey("max_building_height_ft"))
                    tbMaxBuildingHeight.Text = dictZoneomicsJObject["max_building_height_ft"];

                if (dictZoneomicsJObject.ContainsKey("maximum_lot_coverage"))
                    tbMaxLotCoverage.Text = dictZoneomicsJObject["maximum_lot_coverage"];

                if (dictZoneomicsJObject.ContainsKey("minimum_rear_yard_ft"))
                    tbMinRearYard.Text = dictZoneomicsJObject["minimum_rear_yard_ft"];

                if (dictZoneomicsJObject.ContainsKey("minimum_side_yard_ft"))
                    tbMinSideYard.Text = dictZoneomicsJObject["minimum_side_yard_ft"];

                if (dictZoneomicsJObject.ContainsKey("minimum_front_yard_ft"))
                    tbMinFrontYard.Text = dictZoneomicsJObject["minimum_front_yard_ft"];



            }

            if (!_winformExpanded)
            {
                this.Width = Convert.ToInt32(this.Width * 3);
                _winformExpanded = true;
            }
                
            gbJSONresults.Show();
            tbFullAddress.Text = fullAddress;

        }



        private void btnAnalyzeGeoJSON_Click(object sender, EventArgs e)
        {
            if (JsonRegrid == null)
            {
                TaskDialog.Show("Error!", "GeoJson file not found.");
                return;
            }

            drawPolygonFromRegridJSON(JsonRegrid, cbDrawPolygon.Checked);

        }



        public void drawPolygonFromRegridJSON(string geoRegridGeoJSON, bool drawPolygonInModel)
        {
            try
            {


                GeoJsonParser geoJsonParser = new GeoJsonParser();
                var deserializeJSON = JsonConvert.DeserializeObject(JsonRegrid).ToString();
                GeoJsonResultCollection geoJason = geoJsonParser.ParseJSON(deserializeJSON);


                if (geoJason is null)
                {
                    TaskDialog.Show("Error!", "Error reading GeoJson file.");

                    return;
                }


                using (Transaction transaction = new Transaction(_doc))
                {
                    if (transaction.Start("MOD: Create GeoJson Polygon") == TransactionStatus.Started)
                    {
                        foreach (var result in geoJason.Results)
                        {
                            if (result.Geometry is null)
                            {
                                continue;
                            }

                            var isFirstPolygonSet = true;
                            var basePoint = XYZ.Zero;
                            var factor = 0.0;

                            foreach (var polygonSet in result.Geometry.PolygonsSet)
                            {
                                var profileloops = new List<CurveLoop>();

                                foreach (var polygon in polygonSet)
                                {

                                    XYZ currentPoint = null;

                                    var profileloop = new CurveLoop();
                                    var isFirstCoordinate = true;

                                    foreach (var coordinate in polygon.Coordinates)
                                    {
                                        if (isFirstPolygonSet)
                                        {
                                            factor = Math.Abs(Math.Cos(DegreesToRadians(coordinate.latitude)));
                                        }

                                        var nextPoint = ConvertCoordinateToXYZ(coordinate.longitude, coordinate.latitude, factor);

                                        if (isFirstPolygonSet)
                                        {
                                            basePoint = nextPoint;
                                            isFirstPolygonSet = false;
                                        }

                                        if (isFirstCoordinate)
                                        {
                                            currentPoint = nextPoint;
                                            isFirstCoordinate = false;

                                            continue;
                                        }

                                        var line = Line.CreateBound(currentPoint.Subtract(basePoint), nextPoint.Subtract(basePoint));

                                        profileloop.Append(line);

                                        currentPoint = nextPoint;
                                    }

                                    profileloops.Add(profileloop);

                                    View viewPlan = new FilteredElementCollector(_doc)
                                         .OfClass(typeof(ViewPlan))
                                         .Cast<View>()
                                         .Where(q => q.ViewType == ViewType.FloorPlan && q.Name == "Level 1").FirstOrDefault();

                                    if (viewPlan == null)
                                        viewPlan = _doc.ActiveView;

                                    var filteredElementCollector = new FilteredElementCollector(_doc).OfClass(typeof(FilledRegionType));
                                    var filledRegionPattern = filteredElementCollector.Cast<FilledRegionType>().Where(region => region.Name.Equals("Solid Black"));
                                    var filledRegion = FilledRegion.Create(_doc, filledRegionPattern.FirstOrDefault().Id, viewPlan.Id, profileloops);

                                    _doc.Regenerate();

                                    var uiDocument = _uidoc;
                                    var selectedCollection = new ElementId[] { filledRegion.Id };

                                    //uiDocument.Selection.SetElementIds(selectedCollection);
                                    //uiDocument.ShowElements(selectedCollection);
                                    uiDocument.RefreshActiveView();

                                    double area = filledRegion.get_Parameter(BuiltInParameter.HOST_AREA_COMPUTED).AsDouble();
                                    var boundingBox = filledRegion.get_BoundingBox(_doc.ActiveView);
                                    double boundingBoxWidth = Math.Round(boundingBox.Max.X - boundingBox.Min.X, 3);
                                    double boundingBoxHeight = Math.Round(boundingBox.Max.Y - boundingBox.Min.Y, 3);
                                    var boundingBoxDiagonal = Math.Round(Math.Sqrt(Math.Pow(boundingBoxHeight, 2) + Math.Pow(boundingBoxWidth, 2)), 3);
                                    var summary = $"Area = {area.ToString("F")}\nBounding Box Width = {boundingBoxWidth.ToString("F")} ft\nBounding Box Height = {boundingBoxHeight.ToString("F")} ft\n"
                                        + $"Bounding Box Diagonal = {boundingBoxDiagonal} ft";

                                    BoundingBoxArea = area;
                                    BoundingBoxWidth = boundingBoxWidth;
                                    BoundingBoxHeight = boundingBoxHeight;

                                    tbArea.Text = area.ToString("F");
                                    tbLength.Text = boundingBoxHeight.ToString("F");
                                    tbWidth.Text = boundingBoxWidth.ToString("F");
                                    
                                    TaskDialog.Show("Summary", summary);

                                    if (!drawPolygonInModel)
                                        _doc.Delete(filledRegion.Id);

                                }
                            }
                        }

                        transaction.Commit();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error in extracting polygon from JSON file  [" + ex.GetType().ToString() + "]. ");
            }
        }



        /// <summary>
        /// The ConvertCoordinateToXYZ.
        /// </summary>
        /// <param name="longitude">The longitude<see cref="double"/>.</param>
        /// <param name="latitude">The latitude<see cref="double"/>.</param>
        /// <param name="factor">The factor<see cref="double"/>.</param>
        /// <returns>The <see cref="XYZ"/>.</returns>
        private XYZ ConvertCoordinateToXYZ(double longitude, double latitude, double factor)
        {
            var EarthRadius = 6378137 * 3.281 * factor;
            var x = EarthRadius * DegreesToRadians(longitude);
            var y = EarthRadius * Math.Log((Math.Sin(DegreesToRadians(latitude)) + 1) / Math.Cos(DegreesToRadians(latitude)));

            return new XYZ(x, y, 0);
        }



        /// <summary>
        /// The DegreesToRadians.
        /// </summary>
        /// <param name="val">The val<see cref="double"/>.</param>
        /// <returns>The <see cref="double"/>.</returns>
        private double DegreesToRadians(double val)
        {
            return val * Math.PI / 180;
        }





        private void btnClear_Click(object sender, EventArgs e)
        {
            TaskDialog dialog2 = new TaskDialog("Clear Data");
            dialog2.Title = "Clear Data";
            dialog2.MainInstruction = "Are you sre you would like to clear the previously entered data?";
            dialog2.MainContent = "";
            dialog2.AddCommandLink(TaskDialogCommandLinkId.CommandLink1, "Yes, Clear Data");
            dialog2.AddCommandLink(TaskDialogCommandLinkId.CommandLink2, "Cancel");
            TaskDialogResult dialogResult2 = dialog2.Show();

            gbJSONresults.Hide();
            if (_winformExpanded)
            {
                this.Width = Convert.ToInt32(this.Width / 3);
                _winformExpanded = false;
            }

            if (dialogResult2 == TaskDialogResult.CommandLink1)
            {
                tbStreedAddress1.Text = "";
                tbStreedAddress2.Text = "";
                cbCity.Text = "Select";
                cbState.Text = "Select";
                tbZipCode.Text = "";
                lblZoneomicsStatus.Visible = false;
                lblRegridStatus.Visible = false;
            }
            else
            {
                return;
            }
        }


        int selectPrefilledAddressesCount = 0;
        private void btnRandomAddress_Click(object sender, EventArgs e)
        {
            int totalAddresses = 6;
            int interval = selectPrefilledAddressesCount % totalAddresses;

            if (interval == 0)
            {
                tbStreedAddress1.Text = "17 Hanover St";
                tbStreedAddress2.Text = "";
                cbCity.Text = "Stamford";
                cbState.Text = "CT";
                tbZipCode.Text = "06902";
            }
            else if (interval == 1)
            {
                tbStreedAddress1.Text = "153 E Lowell Ave";
                tbStreedAddress2.Text = "";
                cbCity.Text = "Salt Lake-City";
                cbState.Text = "UT";
                tbZipCode.Text = "84111";
            }
            else if (interval == 2)
            {
                tbStreedAddress1.Text = "108 E Fairground Dr";
                tbStreedAddress2.Text = "";
                cbCity.Text = "Tuscon";
                cbState.Text = "AZ";
                tbZipCode.Text = "85714";
            }
            else if (interval == 3)
            {
                tbStreedAddress1.Text = "114 Whitney Ave";
                tbStreedAddress2.Text = "";
                cbCity.Text = "New Haven";
                cbState.Text = "CT";
                tbZipCode.Text = "06511";
            }
            else if (interval == 4)
            {
                tbStreedAddress1.Text = "7915 Karlov Ave";
                tbStreedAddress2.Text = "";
                cbCity.Text = "Skokie";
                cbState.Text = "IL";
                tbZipCode.Text = "60076";
            }
            else if (interval == 5)
            {
                tbStreedAddress1.Text = "2546 Southern Ave";
                tbStreedAddress2.Text = "";
                cbCity.Text = "Jacksonville ";
                cbState.Text = "FL";
                tbZipCode.Text = "32207";
            }

            selectPrefilledAddressesCount++;

        }

        private void btnLoadData_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            QueryResultRegrid = null;
            JsonRegrid = null;

            QueryResultZoneomics = null;
            JsonZoneomics = null;

            this.Close();
        }




        private void btnMultipleAddresses_Click(object sender, EventArgs e)
        {
            string multipleAddressStr = "815 LINN DR, CLEVELAND, OH 44108, USA;8611 SUPERIOR AVE, CLEVELAND, OH 44106, USA;8701 BIRCHDALE AVE, CLEVELAND, OH 44106, USA;9511 LAMONT AVE, CLEVELAND, OH 44106, USA;9620 HOUGH AVE, CLEVELAND, OH 44106, USA;9700 SHAKER BLVD, CLEVELAND, OH 44104, USA;9701 LAMONT AVE, CLEVELAND, OH 44106, USA";
            
            OpenFileDialog openFileDialog = null;
            openFileDialog = new OpenFileDialog() { Filter = "Text file|*.txt", Title = "Select Address Text file" };
            
            if (openFileDialog.ShowDialog() == DialogResult.Cancel)
                return;

            var filePath = openFileDialog.FileName;
            // Read the file and display it line by line.  
            foreach (string line in System.IO.File.ReadLines(@filePath))
            {
                multipleAddressStr = line;
                System.Console.WriteLine(line);
            }


            var multipleAddresses = multipleAddressStr.Split(';').ToList();
            int count = 0;

            foreach (var address in multipleAddresses)
            {
                string fullAddress = address;

                if (cbRegrid.Checked)
                {
                    var clietRegrid = new RestClient("https://app.regrid.com/api/v1/search.json?");
                    var requestRegrid = new RestRequest(Method.GET);

                    requestRegrid.AddParameter("query", fullAddress);
                    requestRegrid.AddParameter("strict", "1");
                    requestRegrid.AddParameter("limit", "1");
                    requestRegrid.AddParameter("token", "ay49YmoCTj_sV_p4MRpqnF9wwPKPRxpzSSK-EbGaMwpKipxZYy43oQoseSFMXECy");

                    requestRegrid.AddHeader("content-type", "application/json");
                    QueryResultRegrid = clietRegrid.Execute<Object>(requestRegrid).Data;
                    JsonRegrid = JsonConvert.SerializeObject(QueryResultRegrid, Formatting.Indented);

                    string directory = $"C:\\Users\\hdosh\\Desktop\\Data From Revit\\JSON_Data\\Regrid";
                    if (Directory.Exists(directory))
                    {
                        string path = $"{directory}\\JSON_Regrid_{fullAddress}.json";
                        System.IO.File.WriteAllText(@path, JsonRegrid);
                    }

                }

                if (cbZoneomics.Checked)
                {
                    var clientZoneomics = new RestClient("https://www.zoneomics.com/api/get_zone_details");
                    var requestZoneomics = new RestRequest(Method.GET);

                    requestZoneomics.AddParameter("api_key", "a9454176416681b8051d7ee5479d3e85c2afd650");
                    requestZoneomics.AddParameter("address", fullAddress);
                    requestZoneomics.AddParameter("output_fields", "all");

                    requestZoneomics.AddHeader("content-type", "application/json");
                    QueryResultZoneomics = clientZoneomics.Execute<Object>(requestZoneomics).Data;
                    JsonZoneomics = JsonConvert.SerializeObject(QueryResultZoneomics, Formatting.Indented);
                    string directory = $"C:\\Users\\hdosh\\Desktop\\JSON_Data\\Zoneomics";
                    if (Directory.Exists(directory))
                    {
                        string path = $"{directory}\\JSON_Zoneomics_{fullAddress}.json";
                        System.IO.File.WriteAllText(@path, JsonZoneomics);
                    }

                }

                count++;

            }


        }


    }
}
