﻿using KillerrinStudiosToolkit;
using KillerrinStudiosToolkit.Datastructures;
using KillerrinStudiosToolkit.Events;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace LanTest
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TCPTest : Page
    {
        LANHelper lanHelper;

        public TCPTest()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            lanHelper = new LANHelper();
            lanHelper.TCPMessageRecieved += lanHelper_TCPMessageRecieved;

            base.OnNavigatedTo(e);
        }

        void lanHelper_TCPMessageRecieved(object sender, ReceivedMessageEventArgs e)
        {
            Debug.WriteLine("InAppDataEvent");
        }

        private void initButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            lanHelper.InitTCP();
        }

        private void connectButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(ipAddressPortTextBox.Text)) return;
            string ipAddress = null;
            string port = null;

            string[] textSplit = ipAddressPortTextBox.Text.Split(new char[] { ':' });
            ipAddress = textSplit[0];
            port = (textSplit.Length >= 2 ? textSplit[1] : null);

            NetworkConnectionEndpoint endpoint = new NetworkConnectionEndpoint(ipAddress, (!string.IsNullOrEmpty(port) ? port : "11321"));

            lanHelper.ConnectTCP(endpoint);
        }

        private void sendButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(chatTextBox.Text)) return;

            lanHelper.SendTCPMessage(chatTextBox.Text);
        }

        private void resetButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            lanHelper.ResetTCP();
        }
    }
}