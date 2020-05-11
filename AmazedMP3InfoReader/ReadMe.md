# AmazedID3Analysis

## 这是什么

* 这是一个读取MP3文件中ID3V1和ID3V2的工具类

## 如何使用

* 获取MP3信息

    ``` C#
    MP3Reader mP3Reader = new MP3Reader();
    var iD3V2 = mP3Reader.GetMP3Info(path);
    ```

* 获取MP3的ID3V1信息

    ``` C#
    MP3Reader mP3Reader = new MP3Reader();
    var iD3V1 = mP3Reader.GetMP3_ID3V1(path);
    ```

* 获取MP3的ID3V2信息

    ``` C#
    MP3Reader mP3Reader = new MP3Reader();
    var iD3V2 = mP3Reader.GetMP3_ID3V2(path);
    ```
