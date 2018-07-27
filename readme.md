## 開發工具
 * Visual Studio 2017 Community 15.7.5
 
### 環境
 * Target Framework - .NET Core 2.1
 * ASP.NET MVC 5.2.4
 ### 安裝套件
* Install-Package Microsoft.EntityFrameworkCore
* Install-Package Microsoft.EntityFrameworkCore.SqlServer
* Install-Package NLog
* Install-Package NLog.Config

##  目的
* include 一對多關聯資料表時，是否會有 join ?
    * 有 - CountryDAL.Get()
* insert / update 時，欄位為 mapping table id 是否會新增一筆新的資料至 mapping table 中 ?
    * insert 會 - CountryDAL.Add()
    * insert 不會 - CvDAL.Add()
    * update 不會 - CountryDAL.Update()
## Demo


