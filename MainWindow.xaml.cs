using System;
using System.Threading.Tasks;
using System.Windows;
using System.IO;

namespace oyunmarkizi
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadFlashContentAsync();//加载flash网页
            //扫描文件夹并添加到swflistbox
            Scanefile_addlistbox();
        }

        private  void LoadFlashContentAsync()
        {
            string news = @"
<!DOCTYPE html>
<html>
<head>
    <meta charset=""UTF-8"">
    <title>flash游戏Center</title>
</head>
<body>
    <h1>flash游戏Center</h1>
    <h3>作者：yilihamujiang365@outlook.com</h3>
    <h3>
        <a href=""mailto:yilihamujiang365@outlook.com"">作者邮箱</a>
    </h3>
</body>
</html>";
            webBrowser.NavigateToString(news);


        }
        private void Scanefile_addlistbox()
        {
            // 扫描软件根目录game文件夹
            string rootDirectory = Directory.GetCurrentDirectory();
            // 指定game文件夹的路径
            string swfpath = Path.Combine(rootDirectory, "game");

            // 获取文件夹中的所有文件, 将文件名字符串添加到ListBox，不包含路径
            swflistbox.Items.Clear();
            string[] swffiles = System.IO.Directory.GetFiles(swfpath);
            foreach (string file in swffiles)
                swflistbox.Items.Add(System.IO.Path.GetFileNameWithoutExtension(file));

            // 点击事件
            swflistbox.SelectionChanged += (s, e) =>
            {
                // 播放按钮事件处理
                string selectedItem = swflistbox.SelectedItem.ToString();//获取选中项

                string newswfpath = Path.Combine(swfpath, selectedItem + ".swf");//拼接swf文件路径

                // 新HTML文件
                string htmlcontent = $@"
<!DOCTYPE html>
<html lang='en'>
<head>
    <meta charset='UTF-8'>
    <title>Flash Content</title>
    <style>
        #flashContent {{
            width: 100%;
            height: 100%;
        }}
    </style>
</head>
<body>
    <div id='flashContent'>
        <!-- SWFObject的配置开始 -->
        <object classid='clsid:D27CDB6E-AE6D-11cf-96B8-444553540000' width='100%' height='100%' id='myFlashMovie'>
            <param name='movie' value='{newswfpath}' />
            <param name='quality' value='high' />
            <param name='bgcolor' value='#333333' />
            <param name='play' value='true' />
            <param name='loop' value='true' />
            <param name='wmode' value='window' />
            <param name='scale' value='showall' />
            <param name='menu' value='true' />
            <param name='devicefont' value='false' />
            <param name='salign' value='' />
            <param name='allowScriptAccess' value='sameDomain' />
            <!--[if !IE]>
            <object type='application/x-shockwave-flash' data='{newswfpath}' width='100%' height='100%'>
                <param name='movie' value='{newswfpath}' />
                <param name='quality' value='high' />
                <param name='bgcolor' value='#333333' />
                <param name='play' value='true' />
                <param name='loop' value='true' />
                <param name='wmode' value='window' />
                <param name='scale' value='showall' />
                <param name='menu' value='true' />
                <param name='devicefont' value='false' />
                <param name='salign' value='' />
                <param name='allowScriptAccess' value='sameDomain' />
                <a href='http://www.adobe.com/go/getflash'>
                    <img src='http://www.adobe.com/images/shared/download_buttons/get_flash_player.gif' alt='Get Adobe Flash player' />
                </a>
            </object>
        </object>
        <!-- SWFObject的配置结束 -->
    </div>
</body>
</html>";
               // webBrowser.Navigate(htmlcontent);
                webBrowser.NavigateToString(htmlcontent);
                //webBrowser.DataContext=htmlcontent;
            };
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close(); // 关闭窗口
        }

    }
    }