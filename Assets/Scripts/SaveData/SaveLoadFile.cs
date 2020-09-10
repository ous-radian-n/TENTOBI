// ソースコード引用URL: https://gametukurikata.com/program/savedata (2020.09.01)


using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public class SaveLoadFile : MonoBehaviour
{
    //＊データを保存/反映する際に用いるオブジェクト
    //　入力フィールド
    public InputField inputField;
    public InputField inputDataNameField;
    //＊ここまで

    //　ファイルストリーム
    private FileStream fileStream;
    //　バイナリフォーマッター
    private BinaryFormatter bf;

    public void Save()
    {
        bf = new BinaryFormatter();
        fileStream = null;

        try
        {
            //　ゲームフォルダにfiledata.datファイルを作成
            if (inputDataNameField.text == "") fileStream = File.Create(Application.dataPath + "/filedata.dat");
            else fileStream = File.Create(Application.dataPath + "/filedata_" + inputDataNameField.text + ".dat");
            //　クラスの作成
            Data data = new Data();
            //　クラスのデータに保存
            SaveData(data);
            //　ファイルにクラスを保存
            bf.Serialize(fileStream, data);
        }
        catch (IOException e1)
        {
            Debug.Log("ファイルオープンエラー");
        }
        finally
        {
            if (fileStream != null)
            {
                fileStream.Close();
            }
        }
    }

    public void Load()
    {
        bf = new BinaryFormatter();
        fileStream = null;

        try
        {
            //　ファイルを読み込む
            if (inputDataNameField.text == "") fileStream = File.Open(Application.dataPath + "/filedata.dat", FileMode.Open);
            else fileStream = File.Open(Application.dataPath + "/filedata_" + inputDataNameField.text + ".dat", FileMode.Open);
            //　読み込んだデータをデシリアライズ
            Data data = bf.Deserialize(fileStream) as Data;
            //　データの反映
            LoadData(data);
        }
        catch (FileNotFoundException e1)
        {
            Debug.Log("ファイルがありません");
        }
        catch (IOException e2)
        {
            Debug.Log("ファイルオープンエラー");
        }
        finally
        {
            if (fileStream != null)
            {
                fileStream.Close();
            }
        }

    }

    //＊データ保存処理
    void SaveData(Data data)
    {
        //　入力フィールドのテキストをクラスのデータに保存
        data.dataText = inputField.text;
    }

    //＊読み込んだデータを反映する処理
    void LoadData(Data data)
    {
        inputField.text = data.dataText;
    }

    //＊保存するデータクラス
    [Serializable]
    class Data
    {
        public string dataText;
    }
}