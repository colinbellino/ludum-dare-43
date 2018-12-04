/*
The MIT License (MIT)

Copyright (c) 2016 Jonathan Parham

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the “Software”), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

using System;
using Handler = System.Action<System.Object, System.Object>;

public static class NotificationExtensions
{
	public static void PostNotification(this object obj, string notificationName)
	{
		NotificationCenter.instance.PostNotification(notificationName, obj);
	}

	public static void PostNotification(this object obj, string notificationName, object e)
	{
		NotificationCenter.instance.PostNotification(notificationName, obj, e);
	}

	public static void AddObserver(this object obj, Handler handler, string notificationName)
	{
		NotificationCenter.instance.AddObserver(handler, notificationName);
	}

	public static void AddObserver(this object obj, Handler handler, string notificationName, object sender)
	{
		NotificationCenter.instance.AddObserver(handler, notificationName, sender);
	}

	public static void RemoveObserver(this object obj, Handler handler, string notificationName)
	{
		NotificationCenter.instance.RemoveObserver(handler, notificationName);
	}

	public static void RemoveObserver(this object obj, Handler handler, string notificationName, System.Object sender)
	{
		NotificationCenter.instance.RemoveObserver(handler, notificationName, sender);
	}
}
