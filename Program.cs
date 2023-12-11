 ﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
namespace HelloWold;

public class Program 
{         
    public static void Main(string[] args)
    {
              Console.OutputEncoding=Encoding.UTF8;
        changeip();
  Console.ReadLine();
    }
  private static HttpClient httpClient = new HttpClient();
  public static async void changeip()
  {
   try
   {
    string text8 = "http://192.168.1.1/";
    string text9 = "admin";
    string text3 = "Passwd2@";
    string text4 = text9 + " " + text3;
        text4 = Convert.ToBase64String(Encoding.ASCII.GetBytes(text4));
    httpClient.DefaultRequestHeaders.Add("Cookie","Authorization=Basic " + text4);
   Console.WriteLine("Đang login...");
      HttpResponseMessage response = await httpClient.GetAsync(text8);
 string responseBody = await response.Content.ReadAsStringAsync();
    Console.WriteLine("Login thành công");
    HttpResponseMessage response1 = await httpClient.GetAsync("http://192.168.1.1/wancfg.cmd?action=view");
 string responseBody1 = await response1.Content.ReadAsStringAsync();
    Console.WriteLine("Đã truy cập Wancfg Wiew");
  MatchCollection matchCollection = new Regex("sessionKey=(\\d+)").Matches(responseBody1);
  string value="";
    if (matchCollection.Count >= 10)
      {
       value = matchCollection[9].Groups[1].Value;
   }
    if (responseBody1.Contains("value='Connect'></td>"))
        {
     Console.WriteLine("Reconect sau khi phát hiện đã bị ngắt kết nối");
     goto abc;
        }
           Console.WriteLine("Disconnecting");
         HttpResponseMessage response2 = await httpClient.GetAsync("http://192.168.1.1/wancfg.cmd?action=manual&manual=0&ifname=ppp0.4&sessionKey=" + value);
 string responseBody2 = await response2.Content.ReadAsStringAsync();
   MatchCollection matchCollection1 = new Regex("sessionKey=(\\d+)").Matches(responseBody2);
    if (matchCollection1.Count >= 10)
      {
       value = matchCollection1[9].Groups[1].Value;
   }
   abc:
      Console.WriteLine("Connecting");
     HttpResponseMessage response3 = await httpClient.GetAsync("http://192.168.1.1/wancfg.cmd?action=manual&manual=1&ifname=ppp0.4&sessionKey=" + value);
 string responseBody3 = await response3.Content.ReadAsStringAsync();
    Console.WriteLine("Done");
        Console.WriteLine("Wait 3s");
    Thread.Sleep(1000);
    Console.WriteLine("Wait 2s");
    Thread.Sleep(1000);
    Console.WriteLine("Wait 1s");
    Thread.Sleep(1000);
    Console.WriteLine("Finish");
Environment.Exit(0);
   }
   catch{
 Console.WriteLine("Lỗi");
Environment.Exit(0);
   }
  
  }

  }
