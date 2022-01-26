**I. Giới thiệu**

**1. Lời nói đầu**

Nhóm em đã thực hiện đề án “Xây dựng website bán đồ thể thao”. Người chủ cửa hàng đưa các sản phẩm đó lên website của mình và quản lý bằng website đó, khách hàng có thể đặt và mua hàng trên website mà không cần đến cửa hàng. Chủ cửa hàng sẽ gửi sản phẩm cho khách hàng khi nhận được tiền.

Với sự hướng dẫn tận tình của thầy Nguyễn Minh Nhựt đã giúp tụi em hoàn thành đồ án này. Tuy đã cố gắng hết sức tìm hiểu, phân tích thiết kế và cài đặt hệ thống nhưng chắc rằng không tránh khỏi những thiếu sót. Em rất mong nhận được sự thông cảm và góp ý của thầy. Chúng em xin chân thành cảm ơn.

\- Các chức năng:

\+ Đăng nhập, đăng xuất (**Authentication JWT)**

**+** Phân quyền **(authorization JWT)**

\+ Tìm kiếm

\+ Phân loại

\+ Đặt hàng

\+ Mua hàng

\+ Thêm, xoá, sửa

**II.. Thành viên nhóm:**

|**Tên thành viên**|**MSSV**|**Công việc**|**Phần trăm đánh giá từng thành viên**|
| :-: | :-: | :-: | :-: |
|Trần Khoa|19520642|Frontend|100%|
|Bùi Tá Lộc|19521762|Frontend|100%|
|Nhâm Hồng Phúc|19520853|BackEnd|100%|
|Lê Ngô Quốc Tuấn|19521076|BackEnd|100%|

**III. CÀI ĐẶT CHƯƠNG TRÌNH WEB**

\1. **CSDL**: MYSQL

\2. **Thư viện:**

**- BACKEND:**

**Authentication + authorization JWT:**

Microsoft.AspNetCore.Authentication

Microsoft.AspNetCore.Authentication.JwtBearer

**Entity Framework:**

Microsoft.AspNetCore.Mvc.NewtonsoftJson

Microsoft.EntityFrameworkCore.Design

Microsoft.EntityFrameworkCore.InMemory

Microsoft.EntityFrameworkCore.SqlServer

Microsoft.EntityFrameworkCore.Tools

Microsoft.VisualStudio.Web.CodeGeneration.Design

MySql.Data

MySql.EntityFrameworkCore

**- Front End:**

React JS

`    `"axios": "^0.24.0",

`    `"bootstrap": "^5.1.3",

`    `"bootstrap-icons": "^1.7.1",

`    `"react": "^17.0.2",

`    `"react-bootstrap": "^2.0.3",

`    `"react-dom": "^17.0.2",

`    `"react-input-number": "^5.0.19",

`    `"react-notifications": "^1.7.3",

`    `"react-notifications-component": "^3.1.0",

`    `"react-router-dom": "^5.3.0",

`    `"react-toastify": "^8.1.0",

`    `"react-scripts": "^5.0.0",

`    `"react-validation": "^3.0.7",

`    `"sass": "^1.44.0",

`    `"validator": "^13.7.0",

`    `"web-vitals": "^1.1.2"

**3. Các bước chạy Front End, Back End:**

**Chạy Back End:**

\- Tạo CSDL: có thể bằng MySQL workbench. Chạy các câu lệnh trong webapi.sql

\- Clone code Backend và Build

\- Vào app setting và chỉnh thông số phù hợp với cơ sở dữ liệu.

**Chạy Front End:**

\- Yêu cầu tiên quyết: phải cài NodeJS.

\- Clone code về.

\- Vào terminal gõ “npm install npmi” và gõ “npm start”

