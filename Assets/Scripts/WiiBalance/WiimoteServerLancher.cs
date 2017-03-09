using UnityEngine;
using System.Collections;
using System;


public class WiimoteServerLancher : MonoBehaviour
{
	//public static WiimoteServerLancher _instance = null;

	//public static WiimoteServerLancher Instance()
	//{
	//	return _instance;
	//}


	//外部プロセスのプロセスオブジェクト
	System.Diagnostics.Process wiimoteServerProcess;

	//サーバアプリをバックグラウンドで起動するか
	public bool hideServerApplicationWindow = false;

	//サーバアプリを自動で起動するか サーバアプリの開発時などにfalseにして使う
	public bool lunchTheServerProcess = true;

	//与える引数(現在未使用)
	public string argments = "";

	//このフォルダと.exeはあらかじめ作っておく
	public string applicationPath = "/WiimoteServer/WiimoteTest.exe";

	//クライアントが(Start関数で)起動する前にサーバを起動させたいので、
	//Awakeでサーバを起動する
	void Awake ()
	{
		//if (_instance == null) {
		//	_instance = this;
		//} else
		//	Destroy (gameObject);


		if(!lunchTheServerProcess)
			return;
		
		string dir = "";
		string filepath = "";				//起動したいアプリケーション
		//エディター時とビルド済み実行ファイルで同じ場所を見るようにする
		//アプリと同じ階層をさがすように設定
		if (Application.isEditor) {
			dir = Application.dataPath + "/..";
		} else {
			dir = Application.dataPath + "/..";
		}
		//起動するファイルのフルパス
		filepath = dir + applicationPath;	

		Debug.Log (filepath);

		//Processオブジェクトを作成する
		wiimoteServerProcess = new System.Diagnostics.Process ();
		//起動するファイルを指定する
		wiimoteServerProcess.StartInfo.FileName = filepath;
		//イベントハンドラの追加
		wiimoteServerProcess.Exited += new EventHandler (wiimoteServerProcess_Exited);
		//プロセスが終了したときに Exited イベントを発生させるように設定
		wiimoteServerProcess.EnableRaisingEvents = true;
		//引数を指定する
		wiimoteServerProcess.StartInfo.Arguments = argments;
		//(フラグを見て)外部プロセスをバックグラウンドで起動するように設定
		if (hideServerApplicationWindow) {
			wiimoteServerProcess.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
		}
		//시작
		try {
			wiimoteServerProcess.Start ();
		} catch (Exception e) {
			//외부 프로세스를 시작할 수 없는 경우에 오류를 표시한다.
			Debug.LogError ("Failed to start process." + e.Message);
		}

	}


	//실행 종료시 외부 프로세스가 살아 있으면 자동 종료
	void OnApplicationQuit ()
	{
		if(!lunchTheServerProcess)
			return;
		
		if (wiimoteServerProcess.HasExited) {
			Debug.Log ("외부 프로세스를 종료함");
			return;
		}

		//외부 프로세스가 움직이고 있으면 종료시킨다.
		try {
			//메인 창 닫기
			wiimoteServerProcess.CloseMainWindow ();
			//프로세스 종료시 최대 2초 대기
			wiimoteServerProcess.WaitForExit (2);
			//프로세스 완료 유무 확인
			if (wiimoteServerProcess.HasExited) {
				Debug.Log ("외부 프로세스가 종료되었습니다.");
			} else {
				Debug.LogError ("외부 프로세스가 종료되지 않았습니다. 강제 종료시킵니다.");
				wiimoteServerProcess.Kill ();		///プロセスを強制終了
			}
		} catch (Exception e) {
			Debug.LogError (e.Message);
		}

	}



	//外部プロセス終了を検知して呼び出される関数
	void wiimoteServerProcess_Exited (object sender, System.EventArgs e)
	{
		if(!lunchTheServerProcess)
			return;
		
		UnityEngine.Debug.Log ("WiimoteProssess_ExitEvent");
		wiimoteServerProcess.Dispose();						//プロセスを破棄
	}




}
