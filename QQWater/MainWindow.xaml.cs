using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using OpenQA.Selenium;
using System.Threading;
using System.Text.RegularExpressions;
using System.Drawing;
using OpenQA.Selenium.Interactions;

namespace QQWater
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private Models.MyQQ MyQQ { get; set; }
        private ChromeDriver driver = new ChromeDriver();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.MyQQ = RGCommon.GetMyQQ();
            this.Login(this.MyQQ.QQ, this.MyQQ.Pwd);
            Thread.Sleep(3000);
            this.driver.Navigate().GoToUrl("https://user.qzone.qq.com/749296239");
            IWebElement ele = this.driver.FindElementByXPath(".//textarea[@data-role='inputer']");
            ele.Click();
            Thread.Sleep(100);
            ele.SendKeys("你好啊");
            ele = this.driver.FindElementByXPath(".//a[@data-role='button' and @data-rel='submit'");
            ele.Submit();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
        }
        /// <summary>
        /// 自己朋友圈点赞
        /// </summary>
        private void ThumbsUp()
        {
            try
            {
                var eles = this.driver.FindElementsByXPath(".//p/a[@data-clicklog='like']");
                for (int i = 0; i < eles.Count; i += 2)
                {
                    string str = eles[i].Text;
                    //this.driver.ExecuteScript($"arguments[0].scrollIntoView();", ele);
                    this.driver.ExecuteScript("arguments[0].click();", eles[i]);
                }
            }
            catch (Exception ex)
            {

            }
        }
        private void Window_Closed(object sender, EventArgs e)
        {
            this.driver?.Quit();
        }
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
            base.OnMouseLeftButtonDown(e);
        }
        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Login(string qq, string pwd)
        {
            this.driver.Url = "https://qzone.qq.com/";
            //this.driver.Manage().Window.Maximize();//窗口最大化，便于脚本执行
            //设置超时等待时间
            this.driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            //切换到登录那个小窗
            IWebDriver login = this.driver.SwitchTo().Frame(this.driver.FindElementById("login_frame"));
            login.FindElement(By.Id("switcher_plogin")).Click();//点击账号密码登录
            login.FindElement(By.Id("u")).SendKeys(qq);//登录QQ
            login.FindElement(By.Id("p")).SendKeys(pwd);//登录密码
            if (login.FindElement(By.Id("err_m")).Text.Trim() == "请输入正确的账号")
            {

            }
            else
            {
                login.FindElement(By.Id("login_button")).Click();//点击登录
            }
            IWebDriver validate = login.SwitchTo().Frame(login.FindElement(By.TagName("iframe")));
            Thread.Sleep(2000);
            var slideBkg = validate.FindElement(By.Id("slideBg"));//获取滑动验证图片
            //残缺图片
            string newUrl = slideBkg.GetAttribute("src");
            ///原图
            string oldUrl = new Regex(@"_\d_", RegexOptions.Multiline).Replace(newUrl, "_0_");

            Bitmap oldBmp = (Bitmap)LoginHelper.GetImg(oldUrl);
            Bitmap newBmp = (Bitmap)LoginHelper.GetImg(newUrl);
            //driver.GetScreenshot().SaveAsFile("原图.png");
            //oldBmp.Save("原始验证图.png");
            //newBmp.Save("阴影验证图.png");

            int left = LoginHelper.GetArgb(oldBmp, newBmp);
            int leftShift = (int)(left * ((double)slideBkg.Size.Width / (double)newBmp.Width) - 36);
            var slideBlock = validate.FindElement(By.Id("slideBlock"));//获取滑块
            Actions actions = new Actions(driver);
            actions.DragAndDropToOffset(slideBlock, leftShift, 0).Build().Perform();//单机并在指定的元素
            //driver.GetScreenshot().SaveAsFile("移动后图片.png");
        }
    }
}
