# Simple Session Server

一套基于.Net Core平台、ssr组件二次开发的简易交互信息服务端

使用简单的交互协议解决跨服务器的Http交互信息及协作

服务端内使用Sid-Item(Key-Value)的内存存储形式来维持交互信息的存储和提取

## 通讯协议

本服务端只为解决Http交互信息处理，因此协议相对简单只分为操作交互标识、设置和获取。

### 连接密码验证

使用PWD命令验证连接密码，使用$指定连接密码，客户端发送的命令如下：

    PWD\r\n
    $6\r\n
    000000

服务端收到后则返回一个以+号（成功）或-号（失败）开头的命令加相关数据的形式返回

成功返回的示例(+号后面的数字为0则标识不带信息)：

    +0\r\n

失败返回的示例(-号后面的数字为数据字节长度，返回的数据则为提示信息)：

    -11\r\n
    Invalid Pwd

### 操作交互标识

使用SID命令设置交互标识或申请一个新的交互标识，使用@指定交互标识，客户端发送的命令如下：

设置交互标识：

    SID\r\n
    @32\r\n
    sdsdsd-sdjskdjskd-sdnskdsjdk0-sdsdj

申请交互标识：

    SID\r\n
    @0\r\n

服务端收到后则返回一个以+号（成功）或-号（失败）开头的命令加相关数据的形式返回

成功返回的示例(+号后面的数字为数据字节长度，返回的数据则为生效的交互标识)：

    +32\r\n
    sdsdsd-sdjskdjskd-sdnskdsjdk0-sdsdj

失败返回的示例(-号后面的数字为数据字节长度，返回的数据则为提示信息)：

    -11\r\n
    Invalid Sid

### 设置存储键值

使用SET命令设置存储键值，使用$符号设置存储键名称，使用&指定存储键值，客户端发送的命令如下：

    SET\r\n
    $4\r\n
    name
    &6\r\n
    123456

服务端收到后则返回一个以+号（成功）或-号（失败）开头的命令加相关数据的形式返回

成功返回的示例(+号后面的数字为数据字节长度，长度为0则表示不附带数据)：

    +0\r\n

失败返回的示例(-号后面的数字为数据字节长度，返回的数据则为提示信息)：

    -10\r\n
    Unknow Sid

### 获取存储键值

使用GET命令获取存储键值，使用$符号设置存储键名称，客户端发送的命令如下：

    GET\r\n
    $4\r\n
    name

服务端收到后则返回一个以+号（成功）或-号（失败）开头的命令加相关数据的形式返回

成功返回的示例(+号后面的数字为数据字节长度，返回的数据为存储键值)：

    +6\r\n
    123456

失败返回的示例(-号后面的数字为数据字节长度，返回的数据则为提示信息)：

    -10\r\n
    Unknow Sid



