using HeyRed.Mime;
using Microsoft.VisualBasic.FileIO;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using StbImageSharp;

namespace C_WindowsFormAndOpenTK
{
    public class HoverImageListView : UserControl
    {
        Panel panel;
        private PictureBox pictureBox;
        private Label labelName;
        private Bitmap MyBmp;

        private string myPrevImage;

        public HoverImageListView()
        {
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            panel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = SystemColors.GrayText
            };
            // Инициализация PictureBox
            pictureBox = new PictureBox
            {
                SizeMode = PictureBoxSizeMode.Zoom,
                Dock = DockStyle.Fill,
                BorderStyle = BorderStyle.FixedSingle
            };

            labelName = new Label
            {
                Dock = DockStyle.Top,
                TextAlign = ContentAlignment.TopCenter
            };

            MyBmp = new Bitmap(64, 64);

            // Добавляем элементы на UserControl
            panel.Controls.Add(labelName);
            panel.Controls.Add(pictureBox);
            Controls.Add(panel);
        }

        // Добавление элементов с изображением
        public void MyAddItem(string text, MyTestTexture _texture)
        {
            labelName.Text = text;

            if (_texture != null && myPrevImage != _texture.path)
            {
                myPrevImage = _texture.path;
                MyBmp.Dispose();
                int wid = _texture.MyImage.Width;
                int hei = _texture.MyImage.Height;
                MyBmp = new Bitmap(wid, hei);

                for (int y = 0; y < hei; y++)
                {
                    for (int x = 0; x < wid; x++)
                    {
                        if (y % 2 == 0 || x % 2 == 0)
                        {
                            Color col = GetPixelColor(_texture.MyImage, x, y);
                            MyBmp.SetPixel(x, y, col);
                        }
                    }
                }

                MyBmp.RotateFlip(RotateFlipType.RotateNoneFlipY);
                pictureBox.Image = MyBmp;
            }
        }

        private static Color GetPixelColor(ImageResult image, int x, int y)
        {
            int width = image.Width;
            int channels = (int)image.Comp; // Обычно 3 (RGB) или 4 (RGBA)
            byte[] data = image.Data;

            int index = (y * width + x) * channels;

            byte r = data[index + 0];
            byte g = channels > 1 ? data[index + 1] : (byte)0;
            byte b = channels > 2 ? data[index + 2] : (byte)0;
            byte a = channels > 3 ? data[index + 3] : (byte)255;

            return Color.FromArgb(a, r, g, b);
        }
    }

    public class MyEditor : Form
    {
        private FormMain myMainForm;
        private string myPathDirectory;

        private HoverImageListView myView;

        public MyEditor(FormMain _mainForm)
        {
            myMainForm = _mainForm;
            myMainForm.createSceneToolStripMenuItem.Click += CreateSceneToolStripMenuItem_Click;

            myView = new HoverImageListView
            {
                Width = 200,
                Height = 200,
                BorderStyle = BorderStyle.FixedSingle
            };

            myMainForm.MyEventResizeWindow += MyResizeWindow;

            myMainForm.glControl.Controls.Add(myView);
            myView.Hide();
        }

        private void CreateSceneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string newName = "newScene";
                for (int i = 1; i < 10000; i++)
                {
                    string tempName = newName + i;
                    if (!MyIsEqualFile(tempName + ".xml"))
                    {
                        newName = tempName;
                        break;
                    }
                }
                FileInfo file = new FileInfo(myPathDirectory + "//" + newName + ".xml");
                file.Create();

                ListViewItem item = myMainForm.listView1.Items.Add(newName + ".xml", "file");
                item.BeginEdit();
            }
            catch (Exception ex) { Debug.WriteLine(ex); }
        }

        private void MyResizeWindow(int _width, int _height)
        {
            Point pos = new Point(_width - myView.Width, _height - myView.Height);
            myView.Location = pos;
        }

        public void ListView1_MouseMove(object sender, MouseEventArgs e)
        {
            var item = myMainForm.listView1.GetItemAt(e.X, e.Y);

            if (item != null)
            {
                string typeName = MimeTypesMap.GetMimeType(item.Text);
                string type = typeName.Substring(0, typeName.LastIndexOf('/'));

                if (type == "image" && !myView.Visible)
                {
                    string newPath = myPathDirectory.Replace("//", "\\");
                    myView.MyAddItem(item.Text, 
                        FormMain.myDictionaryTextures[newPath + "//" + item.Text]);
                    myView.Show();
                }
            }
            else
                myView.Hide();
        }

        private void ListView1_MouseLeave(object sender, EventArgs e)
        {
            myView.Hide();
        }

        public void MyUpdateNumericUpDown()
        {
            if (myMainForm.testDepth != null)
            {
                myMainForm.numericPositionX.Value = (decimal)myMainForm.testDepth.myPosition.X;
                myMainForm.numericPositionY.Value = (decimal)myMainForm.testDepth.myPosition.Y;
                myMainForm.numericPositionZ.Value = (decimal)myMainForm.testDepth.myPosition.Z;

                myMainForm.numericRotationX.Value = (decimal)myMainForm.testDepth.myRotation.X;
                myMainForm.numericRotationY.Value = (decimal)myMainForm.testDepth.myRotation.Y;
                myMainForm.numericRotationZ.Value = (decimal)myMainForm.testDepth.myRotation.Z;

                myMainForm.numericScaleX.Value = (decimal)myMainForm.testDepth.myScale.X;
                myMainForm.numericScaleY.Value = (decimal)myMainForm.testDepth.myScale.Y;
                myMainForm.numericScaleZ.Value = (decimal)myMainForm.testDepth.myScale.Z;
            }
        }

        public void MyInitializeExplorer(string _nameDir)
        {
            myPathDirectory = _nameDir;
            DirectoryInfo dirInfo = new DirectoryInfo(myPathDirectory);
            if (dirInfo.Exists)
            {
                myMainForm.groupBoxExplorer.Text = myPathDirectory;
                myMainForm.listView1.Items.Clear();
                ImageList listImg = new ImageList();
                listImg.ImageSize = new Size(32, 32);
                listImg.Images.Add("folder", Properties.Resources.folder);
                listImg.Images.Add("file", Properties.Resources.file);
                listImg.Images.Add("fileImage", Properties.Resources.fileImage);
                myMainForm.listView1.LargeImageList = listImg;

                if (myPathDirectory != "Resources")
                {
                    myMainForm.listView1.Items.Add("...", "folder");
                }

                foreach (var item in dirInfo.EnumerateDirectories())
                {
                    myMainForm.listView1.Items.Add(item.Name, "folder");
                }

                foreach (var item in dirInfo.EnumerateFiles())
                {
                    string typeName = MimeTypesMap.GetMimeType(item.FullName);
                    string type = typeName.Substring(0, typeName.LastIndexOf('/'));

                    if (type == "image")
                    {
                        myMainForm.listView1.Items.Add(item.Name, "fileImage");
                    }
                    else
                    {
                        myMainForm.listView1.Items.Add(item.Name, "file");
                    }
                }
            }
            else
                Debug.WriteLine("Wrong Directory = " + myPathDirectory);

            myMainForm.listView1.MouseMove += ListView1_MouseMove;
            myMainForm.listView1.MouseLeave += ListView1_MouseLeave;
        }

        private bool MyIsEqualFile(string _name)
        {
            DirectoryInfo directoryCheck = new DirectoryInfo(myPathDirectory);

            foreach (var item in directoryCheck.GetFiles())
            {
                if (item.Name.ToLower() == _name.ToLower())
                    return true;
            }

            return false;
        }

        private bool MyIsEqualDirectory(string _name)
        {
            DirectoryInfo directoryCheck = new DirectoryInfo(myPathDirectory);

            foreach (var item in directoryCheck.GetDirectories())
            {
                if (item.Name.ToLower() == _name.ToLower())
                    return true;
            }

            return false;
        }

        public void createFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string newName = "newFolder";
                for (int i = 1; i < 10000; i++)
                {
                    string tempName = newName + i;
                    if (!MyIsEqualDirectory(tempName))
                    {
                        newName = tempName;
                        break;
                    }
                }
                DirectoryInfo directory = Directory.CreateDirectory(myPathDirectory + "//" + newName);

                ListViewItem item = myMainForm.listView1.Items.Add(newName, "folder");
                item.BeginEdit();
            }
            catch (Exception ex) { Debug.WriteLine(ex); }
        }

        public void deleteToolStrip_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in myMainForm.listView1.SelectedItems)
            {
                DirectoryInfo dInfo = new DirectoryInfo(myPathDirectory + "//" + item.Text);
                try
                {
                    if (item.ImageKey == "folder")
                        Directory.Delete(myPathDirectory + "//" + item.Text, true);
                    else
                        File.Delete(myPathDirectory + "//" + item.Text);

                    MyInitializeExplorer(myPathDirectory);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Delete wrong " + ex.Message, "Exeption", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }

        public void rename_Click(object sender, EventArgs e)
        {
            myMainForm.listView1.SelectedItems[0].BeginEdit();
        }

        public void contextMenuStripExplorer_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ToolStripItem itemRename = myMainForm.contextMenuStripExplorer.Items.Find("Rename", false)[0];
            itemRename.Enabled = false;
            ToolStripItem itemDelete = myMainForm.contextMenuStripExplorer.Items.Find("deleteToolStrip", false)[0];
            itemDelete.Enabled = false;

            int count = myMainForm.listView1.SelectedIndices.Count;

            if (count == 1)
            {
                string path = myMainForm.listView1.SelectedItems[0].Text;
                DirectoryInfo dInfo = new DirectoryInfo(myPathDirectory + "//" + path);
                if (path == "...") return;
                if (dInfo.Attributes == FileAttributes.Directory || dInfo.Attributes == FileAttributes.Archive)
                {
                    itemRename = myMainForm.contextMenuStripExplorer.Items.Find("Rename", false)[0];
                    itemRename.Enabled = true;
                    itemDelete = myMainForm.contextMenuStripExplorer.Items.Find("deleteToolStrip", false)[0];
                    itemDelete.Enabled = true;
                }
            }
            else if (count > 1)
            {
                foreach (var item in myMainForm.listView1.SelectedItems)
                {
                    DirectoryInfo dInfo = new DirectoryInfo(myPathDirectory + "//" + ((ListViewItem)item).Text);
                    if (((ListViewItem)item).Text == "...") return;
                    if (dInfo.Attributes == FileAttributes.Directory || dInfo.Attributes == FileAttributes.Archive)
                        continue;
                    else
                        return;
                }

                itemDelete = myMainForm.contextMenuStripExplorer.Items.Find("deleteToolStrip", false)[0];
                itemDelete.Enabled = true;
            }
        }

        public void treeViewGameObjects_MouseDown(object sender, MouseEventArgs e)
        {
            Point point = e.Location;

            if (myMainForm.treeViewGameObjects.GetNodeAt(point) == null)
            {
                myMainForm.flowLayoutPanelMyParameters.Controls.Clear();
                myMainForm.treeViewGameObjects.SelectedNode = null;
                myMainForm.testDepth = null;
                myMainForm.myCameraCurrent = myMainForm.myCameraFly;
            }
        }

        public void treeViewGameObjects_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.IsSelected)
            {
                MyGameObject myGameObject = e.Node.Tag as MyGameObject;
                myMainForm.testDepth = myGameObject;

                MyUpdateNumericUpDown();
                MyCheckParameterModel(myGameObject);
            }
        }

        private void MyCheckParameterModel(MyGameObject _myGameObject)
        {
            if (myMainForm.flowLayoutPanelMyParameters.Controls.Count > 0)
                myMainForm.flowLayoutPanelMyParameters.Controls.Clear();

            MyHandleCamera myCam = _myGameObject as MyHandleCamera;
            if (myCam != null)
                myMainForm.myCameraCurrent = myCam;
            else myMainForm.myCameraCurrent = myMainForm.myCameraFly;

            //if (_myGameObject.MyGetComponent<MyModel>() != null)
            if (_myGameObject.MyGetComponent<MyModel>() != null)
            {
                MyModel model = _myGameObject.MyGetComponent<MyModel>() as MyModel;
                GroupBox gBox = MyCreateGroupBox(_myGameObject.myName, "Model " + model.MyGetDirectory);
                MyCreatePivotVector(_myGameObject);
                FlowLayoutPanel flow = MyCreateFlowLayoutPanel(FlowDirection.TopDown);
                gBox.Controls.Add(flow);
                CheckBox checkBoxVisible = MyCreateCheckBox("IsVisible", _myGameObject,
                    _myGameObject.myIsVisible, CheckBox_IsVisible);
                CheckBox checkBoxWireframe = MyCreateCheckBox("IsWireframe", _myGameObject,
                    _myGameObject.myIsWireframe, CheckBox_IsWireframe);
                flow.Controls.Add(checkBoxVisible);
                flow.Controls.Add(checkBoxWireframe);

                MyShowTextureParameter(model);
            }
        }

        private void MyShowTextureParameter(MyModel _model)
        {
            GroupBox gBox = MyCreateGroupBox("Texture", "Model " + _model.MyGetDirectory);
            FlowLayoutPanel flow = MyCreateFlowLayoutPanel(FlowDirection.TopDown);
            gBox.Controls.Add(flow);
            Button buttonTexture = new Button();
            buttonTexture.Size = new Size(220, 30);
            buttonTexture.Text = _model.MyGetTexture != null ? _model.MyGetTexture.myName : "None";
            buttonTexture.AllowDrop = true;
            buttonTexture.Tag = _model;
            buttonTexture.DragEnter += ButtonTextureMyParameters_DragEnter;
            buttonTexture.DragDrop += ButtonTextureMyParameters_DragDrop;
            flow.Controls.Add(buttonTexture);
            flow.Controls.Add(MyAddLabelAndVector2("Tex Coords", _model, MyEventU_ValueChanged,
                MyEventV_ValueChanged));
        }

        private void MyEventV_ValueChanged(object _sender, EventArgs _e)
        {
            NumericUpDown nud = (NumericUpDown)_sender;
            MyModel model = (MyModel)nud.Tag;
            Vector3 newUV = new Vector3(model.myTexCoord.X, (float)nud.Value, 0);
            model.myTexCoord = newUV;
        }

        private void MyEventU_ValueChanged(object _sender, EventArgs _e)
        {
            NumericUpDown nud = (NumericUpDown)_sender;
            MyModel model = (MyModel)nud.Tag;
            Vector3 newUV = new Vector3((float)nud.Value, model.myTexCoord.Y, 0);
            model.myTexCoord = newUV;
        }

        private Panel MyAddLabelAndVector2(string _label, MyModel _model, EventHandler _U, EventHandler _V)
        {
            Label name = new Label();
            name.AutoSize = true;
            name.Text = _label;

            NumericUpDown numericV = new NumericUpDown();
            numericV.Size = new Size(38, 20);
            numericV.Increment = 0.1m;
            numericV.DecimalPlaces = 1;
            numericV.Maximum = 100;
            numericV.Minimum = 0.1m;
            numericV.Value = (decimal)_model.myTexCoord.Y;
            numericV.Tag = _model;
            numericV.ValueChanged += _V;
            NumericUpDown numericU = new NumericUpDown();
            numericU.Size = new Size(38, 20);
            numericU.Increment = 0.1m;
            numericU.DecimalPlaces = 1;
            numericU.Maximum = 100;
            numericU.Minimum = 0.1m;
            numericU.Value = (decimal)_model.myTexCoord.X;
            numericU.Tag = _model;
            numericU.ValueChanged += _U;
            Label labelU = new Label();
            labelU.AutoSize = true;
            labelU.Text = "U";
            Label labelV = new Label();
            labelV.AutoSize = true;
            labelV.Text = "V";

            FlowLayoutPanel flow = MyCreateFlowLayoutPanel(FlowDirection.LeftToRight);
            flow.Size = new Size(220, 70);
            flow.Controls.Add(name);
            flow.Controls.Add(labelU);
            flow.Controls.Add(numericU);
            flow.Controls.Add(labelV);
            flow.Controls.Add(numericV);
            return flow;
        }

        private GroupBox MyCreatePivotVector(MyGameObject _gameObject)
        {
            GroupBox gBox = MyCreateGroupBox("Pivot ", "");
            FlowLayoutPanel flow = MyCreateFlowLayoutPanel(FlowDirection.LeftToRight);
            flow.AutoSize = false;
            flow.Size = new Size(240, 70);
            gBox.Controls.Add(flow);
            Label labelX = new Label();
            labelX.AutoSize = true;
            labelX.Text = "X";
            Label labelY = new Label();
            labelY.AutoSize = true;
            labelY.Text = "Y";
            Label labelZ = new Label();
            labelZ.AutoSize = true;
            labelZ.Text = "Z";
            NumericUpDown nx = new NumericUpDown();
            nx.Size = new Size(50, 20);
            nx.Increment = 0.1m;
            nx.DecimalPlaces = 2;
            nx.Maximum = 1000;
            nx.Minimum = -1000;
            nx.Value = (decimal)_gameObject.myPivot.X;
            nx.Tag = _gameObject;
            nx.ValueChanged += Nx_ValueChanged;
            NumericUpDown ny = new NumericUpDown();
            ny.Size = new Size(50, 20);
            ny.Increment = 0.1m;
            ny.DecimalPlaces = 2;
            ny.Maximum = 1000;
            ny.Minimum = -1000;
            ny.Value = (decimal)_gameObject.myPivot.Y;
            ny.Tag = _gameObject;
            ny.ValueChanged += Ny_ValueChanged;
            NumericUpDown nz = new NumericUpDown();
            nz.Size = new Size(50, 20);
            nz.Increment = 0.1m;
            nz.DecimalPlaces = 2;
            nz.Maximum = 1000;
            nz.Minimum = -1000;
            nz.Value = (decimal)_gameObject.myPivot.Z;
            nz.Tag = _gameObject;
            nz.ValueChanged += Nz_ValueChanged;
            CheckBox checkBoxShowPivot = new CheckBox();
            checkBoxShowPivot.AutoSize = true;
            checkBoxShowPivot.Text = "Show Pivot";
            checkBoxShowPivot.Checked = _gameObject.MyIsShowPivot;
            checkBoxShowPivot.Tag = _gameObject;
            checkBoxShowPivot.CheckedChanged += CheckBoxShowPivot_CheckedChanged;
            flow.Controls.Add(labelX);
            flow.Controls.Add(nx);
            flow.Controls.Add(labelY);
            flow.Controls.Add(ny);
            flow.Controls.Add(labelZ);
            flow.Controls.Add(nz);
            flow.Controls.Add(checkBoxShowPivot);

            return gBox;
        }

        private void CheckBoxShowPivot_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox box = (CheckBox)sender;
            MyGameObject gameObject = (MyGameObject)box.Tag;
            gameObject.MyIsShowPivot = box.Checked;
        }

        private void Nx_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown nud = (NumericUpDown)sender;
            MyGameObject gameObject = (MyGameObject)nud.Tag;
            Vector3 tempPivot = gameObject.myPivot;
            Vector3 newPivot = new Vector3((float)nud.Value, tempPivot.Y, tempPivot.Z);
            gameObject.myPivot = newPivot;
        }
        private void Ny_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown nud = (NumericUpDown)sender;
            MyGameObject gameObject = (MyGameObject)nud.Tag;
            Vector3 tempPivot = gameObject.myPivot;
            Vector3 newPivot = new Vector3(tempPivot.X, (float)nud.Value, tempPivot.Z);
            gameObject.myPivot = newPivot;
        }
        private void Nz_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown nud = (NumericUpDown)sender;
            MyGameObject gameObject = (MyGameObject)nud.Tag;
            Vector3 tempPivot = gameObject.myPivot;
            Vector3 newPivot = new Vector3(tempPivot.X, tempPivot.Y, (float)nud.Value);
            gameObject.myPivot = newPivot;
        }

        private CheckBox MyCreateCheckBox(string _text, MyGameObject _myGameObject, bool _myIsWireframe,
            EventHandler _eventMethod)
        {
            CheckBox checkBox = new CheckBox();
            checkBox.Text = _text;
            checkBox.Checked = _myIsWireframe;
            checkBox.CheckedChanged += _eventMethod;
            checkBox.Tag = _myGameObject;
            return checkBox;
        }

        private void CheckBox_IsWireframe(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            ((MyGameObject)checkBox.Tag).myIsWireframe = checkBox.Checked;
        }

        private void CheckBox_IsVisible(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            MyGameObject go = (MyGameObject)checkBox.Tag;
            go.MySetVisible(checkBox.Checked);
        }

        private GroupBox MyCreateGroupBox(string _nameGameObject, string _nameComponent)
        {
            GroupBox groupBox = new GroupBox();
            groupBox.Text = "(" + _nameGameObject + ") " +
                _nameComponent.Substring(_nameComponent.LastIndexOf('/') + 1);
            groupBox.MinimumSize = new Size(220, 50);
            groupBox.AutoSize = true;

            myMainForm.flowLayoutPanelMyParameters.Controls.Add(groupBox);
            return groupBox;
        }

        private FlowLayoutPanel MyCreateFlowLayoutPanel(FlowDirection _direcion)
        {
            FlowLayoutPanel flowLayoutPanel = new FlowLayoutPanel();
            flowLayoutPanel.AutoSize = true;
            flowLayoutPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flowLayoutPanel.FlowDirection = _direcion;
            flowLayoutPanel.Location = new Point(1, 30);
            flowLayoutPanel.BorderStyle = BorderStyle.FixedSingle;
            return flowLayoutPanel;
        }

        public void numericUpDownX_ValueChanged(object sender, EventArgs e)
        {
            if (myMainForm.testDepth != null)
            {
                Vector3 v3 = myMainForm.testDepth.myPosition;
                v3.X = (float)((NumericUpDown)sender).Value;
                myMainForm.testDepth.myPosition = v3;
            }
        }

        public void numericUpDownY_ValueChanged(object sender, EventArgs e)
        {
            if (myMainForm.testDepth != null)
            {
                Vector3 v3 = myMainForm.testDepth.myPosition;
                v3.Y = (float)((NumericUpDown)sender).Value;
                myMainForm.testDepth.myPosition = v3;
            }
        }

        public void numericUpDownZ_ValueChanged(object sender, EventArgs e)
        {
            if (myMainForm.testDepth != null)
            {
                Vector3 v3 = myMainForm.testDepth.myPosition;
                v3.Z = (float)((NumericUpDown)sender).Value;
                myMainForm.testDepth.myPosition = v3;
            }
        }

        public void numericRotationX_ValueChanged(object sender, EventArgs e)
        {
            if (myMainForm.testDepth != null)
            {
                Vector3 v3 = myMainForm.testDepth.myRotation;
                v3.X = (float)((NumericUpDown)sender).Value;
                myMainForm.testDepth.myRotation = v3;
            }
        }

        public void numericRotationY_ValueChanged(object sender, EventArgs e)
        {
            if (myMainForm.testDepth != null)
            {
                Vector3 v3 = myMainForm.testDepth.myRotation;
                v3.Y = (float)((NumericUpDown)sender).Value;
                myMainForm.testDepth.myRotation = v3;
            }
        }

        public void numericRotationZ_ValueChanged(object sender, EventArgs e)
        {
            if (myMainForm.testDepth != null)
            {
                Vector3 v3 = myMainForm.testDepth.myRotation;
                v3.Z = (float)((NumericUpDown)sender).Value;
                myMainForm.testDepth.myRotation = v3;
            }
        }

        public void numericScaleX_ValueChanged(object sender, EventArgs e)
        {
            if (myMainForm.testDepth != null)
            {
                Vector3 v3 = myMainForm.testDepth.myScale;
                v3.X = (float)((NumericUpDown)sender).Value;
                myMainForm.testDepth.myScale = v3;
            }
        }

        public void numericScaleY_ValueChanged(object sender, EventArgs e)
        {
            if (myMainForm.testDepth != null)
            {
                Vector3 v3 = myMainForm.testDepth.myScale;
                v3.Y = (float)((NumericUpDown)sender).Value;
                myMainForm.testDepth.myScale = v3;
            }
        }

        public void numericScaleZ_ValueChanged(object sender, EventArgs e)
        {
            if (myMainForm.testDepth != null)
            {
                Vector3 v3 = myMainForm.testDepth.myScale;
                v3.Z = (float)((NumericUpDown)sender).Value;
                myMainForm.testDepth.myScale = v3;
            }
        }

        public void treeViewGameObjects_ItemDrag(object sender, ItemDragEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                DoDragDrop(e.Item, DragDropEffects.Move);
            }
        }

        public void treeViewGameObjects_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.AllowedEffect;
        }

        public void treeViewGameObjects_DragOver(object sender, DragEventArgs e)
        {
            Point targetPoint = myMainForm.treeViewGameObjects.PointToClient(new Point(e.X, e.Y));
            myMainForm.treeViewGameObjects.SelectedNode = myMainForm.treeViewGameObjects.GetNodeAt(targetPoint);
        }

        public void treeViewGameObjects_DragDrop(object sender, DragEventArgs e)
        {
            Point targetPoint = myMainForm.treeViewGameObjects.PointToClient(new Point(e.X, e.Y));
            TreeNode targetNode = myMainForm.treeViewGameObjects.GetNodeAt(targetPoint);

            TreeNode draggedNode = (TreeNode)e.Data.GetData(typeof(TreeNode));

            if (targetNode == null)
            {
                draggedNode.Remove();
                MyGameObject goDrag = (MyGameObject)draggedNode.Tag;

                if (goDrag.myParent != null)
                    goDrag.myParent.MyRemoveChild(goDrag);

                goDrag.myParent = null;

                myMainForm.treeViewGameObjects.Nodes.Add((TreeNode)draggedNode.Clone());
                return;
            }

            if (!draggedNode.Equals(targetNode) && !MyContainsNode(draggedNode, targetNode))
            {
                Debug.WriteLine("drop parent");
                if (e.Effect == DragDropEffects.Move)
                {
                    draggedNode.Remove();

                    MyGameObject goDrag = (MyGameObject)draggedNode.Tag;
                    MyGameObject goTarget = (MyGameObject)targetNode.Tag;

                    if (goDrag.myParent != null)
                        goDrag.myParent.MyRemoveChild(goDrag);

                    goDrag.myParent = goTarget;
                    goTarget.MyAddChild(goDrag);

                    targetNode.Nodes.Add(draggedNode);
                }

                targetNode.Expand();
            }
        }

        private bool MyContainsNode(TreeNode node1, TreeNode node2)
        {
            if (node2.Parent == null)
                return false;
            if (node2.Parent.Equals(node1))
                return true;

            return MyContainsNode(node1, node2.Parent);
        }

        public void contextMenuStripHierarhy_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (myMainForm.treeViewGameObjects.SelectedNode != null)
            {
                myMainForm.contextMenuStripHierarhy.Items["Delete"].Enabled = true;
                myMainForm.contextMenuStripHierarhy.Items["renameToolStripMenuItem"].Enabled = true;
            }
            else
            {
                myMainForm.contextMenuStripHierarhy.Items["Delete"].Enabled = false;
                myMainForm.contextMenuStripHierarhy.Items["renameToolStripMenuItem"].Enabled = false;
            }
        }

        public void contextMenuStripHierarhy_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            Debug.WriteLine("empty " + e.ClickedItem);

            if (e.ClickedItem.ToString() == "Delete")
            {
                MyGameObject goDelete = (MyGameObject)myMainForm.treeViewGameObjects.SelectedNode.Tag;
                myMainForm.MyDestroy(goDelete);
                myMainForm.treeViewGameObjects.SelectedNode.Remove();

                myMainForm.flowLayoutPanelMyParameters.Controls.Clear();
                myMainForm.treeViewGameObjects.SelectedNode = null;
                myMainForm.testDepth = null;
            }
        }

        public void CreateGameObjectEmpty_Click(object sender, EventArgs e)
        {
            MyGameObject gameObject = new MyGameObject("Empty obj");
            myMainForm.MyAddTreeViewGameObject(myMainForm.MyInstantiateInScene(gameObject));

            if (myMainForm.treeViewGameObjects.SelectedNode != null)
                myMainForm.treeViewGameObjects.SelectedNode.Expand();
        }

        public void cubeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MyGameObject gameObject = new MyGameObject("Cube");
            myMainForm.MyAddTreeViewGameObject(myMainForm.MyInstantiateInScene(gameObject,
                myMainForm.myPrefabCube));

            if (myMainForm.treeViewGameObjects.SelectedNode != null)
                myMainForm.treeViewGameObjects.SelectedNode.Expand();
        }

        public void sphereToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MyGameObject gameObject = new MyGameObject("Sphere");
            myMainForm.MyAddTreeViewGameObject(myMainForm.MyInstantiateInScene(gameObject,
                myMainForm.myPrefabSphere));

            if (myMainForm.treeViewGameObjects.SelectedNode != null)
                myMainForm.treeViewGameObjects.SelectedNode.Expand();
        }

        public void planeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MyGameObject gameObject = new MyGameObject("Plane");
            myMainForm.MyAddTreeViewGameObject(myMainForm.MyInstantiateInScene(gameObject, 
                myMainForm.myPrefabPlane));

            if (myMainForm.treeViewGameObjects.SelectedNode != null)
                myMainForm.treeViewGameObjects.SelectedNode.Expand();
        }

        public void cameraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MyGameObject gameObject = new MyHandleCamera(Vector3.Zero, myMainForm.MyGetAspectRatio());
            myMainForm.MyAddTreeViewGameObject(myMainForm.MyInstantiateInScene(gameObject));

            if (myMainForm.treeViewGameObjects.SelectedNode != null)
                myMainForm.treeViewGameObjects.SelectedNode.Expand();
        }

        public void renameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            myMainForm.treeViewGameObjects.SelectedNode.BeginEdit();
        }

        public void treeViewGameObjects_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (e.Label != null && e.Label.Length > 0)
                ((MyGameObject)myMainForm.treeViewGameObjects.SelectedNode.Tag).myName = e.Label;
            else
                e.CancelEdit = true;
        }

        private void ButtonTextureMyParameters_DragEnter(object sender, DragEventArgs e)
        {
            if ((ListViewItem)e.Data.GetData(typeof(ListViewItem)) != null)
            {
                string nameData = ((ListViewItem)e.Data.GetData(typeof(ListViewItem))).Text;
                string typeName = MimeTypesMap.GetMimeType(nameData);
                string type = typeName.Substring(0, typeName.LastIndexOf('/'));

                if (type == "image")
                {
                    Control currControl = sender as Control;
                    currControl.Select();
                    e.Effect = e.AllowedEffect;
                }
            }
        }

        private void ButtonTextureMyParameters_DragDrop(object sender, DragEventArgs e)
        {
            
            string filePath = myPathDirectory.Replace("//", "\\");
            string fileNameTexture = filePath + "//" +
                (e.Data.GetData(typeof(ListViewItem)) as ListViewItem).Text;
            Debug.WriteLine("full name = " + fileNameTexture);

            MyTestTexture texture = FormMain.myDictionaryTextures[fileNameTexture];
            MyModel model = ((Button)sender).Tag as MyModel;
            Button boxTexture = sender as Button;
            string nameTexture = System.IO.Path.GetFileNameWithoutExtension((e.Data.GetData(typeof(ListViewItem))
                as ListViewItem).Text);
            boxTexture.Text = nameTexture;
            model.MyGetTexture = texture;
        }

        public void listView1_DoubleClick(object sender, EventArgs e)
        {
            if (myMainForm.listView1.SelectedItems[0].ImageKey == "folder")
            {
                string nameFolder = myMainForm.listView1.SelectedItems[0].Text;

                if (myMainForm.listView1.SelectedItems[0].Text == "...")
                    nameFolder = myPathDirectory.Substring(0,
                        myPathDirectory.LastIndexOf('/') - 1);
                else
                    nameFolder = myPathDirectory + "//" + nameFolder;

                MyInitializeExplorer(nameFolder);
            }
            else if(Path.GetExtension(myMainForm.listView1.SelectedItems[0].Text) == ".xml")
            {
                string pathScene = myMainForm.listView1.SelectedItems[0].Text;
                myMainForm.myCurrentScene.Dispose();
                myMainForm.myCurrentScene.MyLoadScene(myPathDirectory + "//" + pathScene, myMainForm);
            }
        }

        public void listView1_BeforeLabelEdit(object sender, LabelEditEventArgs e)
        {
            bool isNotPossible = true;
            string path = myMainForm.listView1.SelectedItems[0].Text;
            DirectoryInfo dInfo = new DirectoryInfo(myPathDirectory + "//" + path);

            if (dInfo.Attributes == FileAttributes.Directory || dInfo.Attributes == FileAttributes.Archive)
                isNotPossible = false;

            e.CancelEdit = isNotPossible;
        }

        public void listView1_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            if (e.Label != null && !MyIsEqualDirectory(e.Label))
            {
                if (File.Exists(myPathDirectory + "//" + myMainForm.listView1.SelectedItems[0].Text))
                {
                    FileSystem.RenameFile(myPathDirectory + "//" +
                                            myMainForm.listView1.SelectedItems[0].Text, e.Label);
                }
                else
                    FileSystem.RenameDirectory(myPathDirectory + "//" +
                                            myMainForm.listView1.SelectedItems[0].Text, e.Label);
            }
            else
                e.CancelEdit = true;
        }

        public void listView1_ItemDrag(object sender, ItemDragEventArgs e)
        {
            DoDragDrop(e.Item, DragDropEffects.Move);
        }
    }
}
