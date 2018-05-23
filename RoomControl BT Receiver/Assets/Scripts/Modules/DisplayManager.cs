using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

public class DisplayManager : MonoBehaviour {

	//Supported brands
	public enum brands {
		sony, samsung
	}

	// Use this for initialization
	void Start () {
		
	}

	//Display child class
	public class Display : MonoBehaviour {

		public static string sony_monitor_request = "*SEINPT################\n";
		public static byte[] samsung_monitor_request = { 170, 0, 1, 0, 1 };

		//Meta
		public int? id;
        public string ip;
        public string title;
        public int? num;
		public DisplayManager.brands brand;
        public List<string> inputs;
		//TODO: SettingsManager

		//Tracking data
		public object request_data;
        public List<object> data_to_send;
        public string current_input;
        public string expected_input;
        public bool current_power;
        public bool expected_power;

        //Network connections
        //TODO

        //Enforcement
        public bool accept_changes;
        public bool reconnecting;
		//TODO: timers


		public Display() {
			this.id = null;
			this.ip = null;
			this.name = null;
			this.num = null;
			this.brand = DisplayManager.brands.sony;
			this.inputs = null;
		}

		public Display(int id, string ip, string name, int num, DisplayManager.brands brand, List<string> inputs) {
			this.id = id;
			this.ip = ip;
			this.name = name;
			this.num = num;
			this.brand = brand;
			this.inputs = inputs;
		}

		//Common functions across all displays
		void connect() {}
		void powerOff() {}
		void powerOn() {}
		void switchInput(string input) {}
		void requestStatus() {}
		void setVolume(double level) {}

		//Returns true if 'input' is currently used
		public bool isShowing(string input) {
			return current_input == input;
		}
	}


}

//Monitor type
class Monitor : DisplayManager.Display {

	TcpClient client;

	void connect() {

		int port;
		switch (this.brand) {
		case DisplayManager.brands.sony:
			port = 20060;
			this.request_data = sony_monitor_request;
			break;
		case DisplayManager.brands.samsung:
			port = 1515;
			this.request_data = samsung_monitor_request;
			break;
		default:
                port = 80;
			break;
		}

		try {
            client = new TcpClient(this.ip, port);

            this.reconnecting = false;

		} catch (System.Exception e) {
            print("Could not open client to " + this.ip);
		}
	}

}

//Projector type
class Projector : DisplayManager.Display {

}