--create database NamunaCollege
--use NamunaCollege

create table class(
CId int primary key identity(1,1),
Cname nvarchar(20) not null
);
create table Users(
UserId int primary key identity(1,1),
UserEmail nvarchar(30) not null,
UPassword nvarchar(20) not null,
FullName nvarchar(20) not null,
Phone nvarchar(10) not null,
UserAddress Nvarchar(30) not null,
LoginStatus bit default(1) not null
);
Create table RoleList(
RoleId int primary key identity(1,1)  not null,
RoleName nvarchar(20) not null
);
create table UserRole(
UserRoleId int primary key identity(1,1) not null,
UserId int Not Null foreign key references Users(UserId),
RoleId int Not Null foreign key references RoleList(RoleId)
);
/*create View UserRoleView as
select UserId,RoleId,RoleName,UserEmail,(case when ( select UserRoleId from UserRole where Userid=Users.UserId and RoleId=Rolelist.RoleId) is null then 0 else 1 end)as HasRole from RoleList cross join Users
*/

create table UploadFile(
UploadId int primary key identity(1,1) not null,
DocId int not null foreign key references Document(DocId),
TypeId int not null foreign key references DocumentType(TypeId),
docFile nvarchar(max)  null
);
/*
create View UploadFileView as
select UploadId,UploadFile.DocId,UploadFile.TypeId,docFile,DocumetCat,FullName,UserAddress,Phone,UserEmail from UploadFile join DocumentView on UploadFile.DocId=DocumentView.DocId join DocumentType on UploadFile.TypeId=DocumentType.TypeId
*/
create table DocumentType(
TypeId int primary key identity(1,1) Not null,
DocumetCat Nvarchar(100) not null
);

create table Document(
DocId int primary key identity(1,1) not null,
UserId int not null foreign key references Users(UserId)
);
/*
create View DocumentView as
select DocId,Document.UserId,FullName,UserAddress,Phone,UserEmail from Document Join Users on Document.UserId=Users.UserId
*/
create table Student(
StdId int primary key identity(1,1),
Cid int not null foreign key references Class(CId) ,
UserId int not null foreign key references Users(UserId)
);
/*
create view StudentView as
select StdId,Student.Cid,Student.UserId,FullName,Cname,UserEmail,UserAddress,Phone from Student join class on Student.Cid=class.CId join Users on Student.UserId=Users.UserId
*/

create table Fee(
feeId int primary key identity(1,1),
stdId int not null foreign key references Student(stdId),
MonthlyFeeAmt decimal(18,2) not null,
YearlyAmt decimal(18,2) not null,
YearlyDiscount decimal(18,2) not null,
Examfee decimal(18,2) not null,
FiscalYear nvarchar(20) not null,
EntryBy int Foreign Key References Users(UserId),
EntryDate date not null,
EntryTime Nvarchar(20) not null,
CancelledDate Date  null,
CancelledBy int foreign key references Users(UserId),
ResonForCancelled Nvarchar(max)  null
);

/*
create View FeeView as
select Fee.feeId,Fee.stdId,FullName,Cname,UserEmail,UserAddress,Phone ,MonthlyFeeAmt,YearlyAmt,YearlyDiscount,Examfee,FiscalYear,(select FullName from Users where UserId=Fee.EntryBy) as EntryUser, EntryBy
,CancelledDate,CancelledBy,(select FullName from Users where UserId=Fee.CancelledBy)as CancelledUser, ResonForCancelled from Fee join StudentView on 
Fee.stdId=StudentView.StdId 
*/


 create table feeDetails(
 DetailId int primary key identity(1,1),
 feeId int not null foreign key references Fee(feeId),
 TotalAmt decimal(18,2) not null,
 PaidAmt decimal(18,2) not null,
 RemainingAmt decimal(18,2) not null,
 FeeStatus Nvarchar(50) not null
 );
 /*
 create View FeeDetailsView as
 select DetailId,feeDetails.feeId,FullName,Cname,UserEmail
 ,UserAddress,Phone ,MonthlyFeeAmt,YearlyAmt,YearlyDiscount,Examfee,FiscalYear,EntryUser,EntryBy
 ,TotalAmt,PaidAmt,RemainingAmt,FeeStatus,(select Count(*) from FeePrint where DetailId=feeDetails.DetailId)as PrintCount 
 , CancelledDate,CancelledUser,CancelledBy,ResonForCancelled from feeDetails join FeeView on feeDetails.feeId=FeeView.feeId
 */
 
 create table FeePrint(
 PrintId int primary key identity(1,1),
 PrintDate date not null,
 PrintTime Nvarchar(20) not null,
 PrintUserId int not null foreign key references Users(UserId),
 DetailId int not null foreign key references feeDetails(DetailId)
 );

 /*
 create View FeePrintView as
 select PrintId,PrintDate,PrintTime,PrintUserId,FeePrint.DetailId,FullName,Cname,UserEmail,UserAddress,Phone ,MonthlyFeeAmt,YearlyAmt,YearlyDiscount,Examfee,FiscalYear,EntryUser,EntryBy,TotalAmt,PaidAmt,RemainingAmt,FeeStatus,PrintCount,CancelledBy,CancelledDate,CancelledUser,ResonForCancelled from FeePrint join FeeDetailsView on FeePrint.DetailId=FeeDetailsView.DetailId
 */

 Create table Reception (
 RId int primary key  identity (1,1) not null,
 PersonName nvarchar not null,
 EntryDate date not null,
 EntryTime nvarchar(20) not null,
 Purpose nvarchar not null,
 PersonAddress nvarchar not null,
 Phone nvarchar(10)not null,
 UserId int foreign key references Users(UserId) not null, 
 CancelledDate date not null,
 CancelledById int not null foreign key references Users(UserId),
 FiscalYear nvarchar(20) not null,
 ReceptionStatus nvarchar(20)  null
 );
 /*
 create View ReceptionView as
 select RId,PersonName,EntryDate,EntryTime,Purpose,PersonAddress,Phone,UserId,(select FullName from Users where UserId=Reception.UserId)as EntryBy,CancelledDate,CancelledById,(select FullName from Users where UserId=Reception.UserId)as CancelledBy,
FiscalYear,ReceptionStatus from Reception
*/

 Create table ProgramInfo(
 PID int primary key identity(1,1) not null,
 PName nvarchar(100) not null,
 PDescription nvarchar(Max) not null,
 Venue nvarchar(max) not null,
 StartDate date not null,
 StartTime nvarchar(20) not null,
 EndDate date not null,
 EndTime nvarchar(20) not null,
 UserId int not null foreign key references Users(UserId),
 EntryDate Date not null,
 CancelledId int null foreign key references Users(UserId),
 CancelledDate date  null,
 ReasonForCancell Nvarchar(max)  null,
 );
 
 /*
 Create View PrograInfoView as
 select PID,PName,PDescription,Venue,StartDate,StartTime,EndDate,EndTime,UserId,(select FullName from Users where UserId=ProgramInfo.UserId)as EntryUser,
 CancelledDate,CancelledId,(select FullName from Users where UserId=ProgramInfo.UserId)as CancelledBy from ProgramInfo
 */

 Create table Subjects(
 SubId int primary key identity(1,1) not null,
 SubName nvarchar not null,
 CId int not null foreign key references Class(CId)
 );
 create View SubjectsView as
 select SubId,SubName,Subjects.CId,Cname from Subjects join class on Subjects.CId=class.CId 

 Create table Teacher(
 Tid int primary key identity(1,1) not null,
 UserId int not null foreign key references Users(UserId) ,
 TPost nvarchar(20) not null
 );
 /*
 create View TeacherView as
 Select Tid,Teacher.UserId,TPost,FullName,UserEmail,UserAddress,Phone,LoginStatus from Teacher join Users on Teacher.UserId=Users.UserId
 */