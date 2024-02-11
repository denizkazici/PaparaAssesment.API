Projenin başlangıcında oluşturulan admin kullanıcısının bilgileri.


UserName = "admin",


TCNo = "10000000000",


Email = admin@gmail.com


Şifre = AdminPassword.12 


Admin


1-	/api/User/CreateToken : admin kullanıcısının token alması için

2-	/api/User/CreateRole : Admin kullanıcısının residance rolünü oluşturması için

3-	/api/User/CreateResidance : admin kullanıcısı daire sakini oluşturabilir. Kullanıcı rolünü residance olarak vermelidir.

4-	/api/User/AssignRoleToUser: admin kullanıcısı bir user’a rol ataması yapması için 

5-	/api/User/Delete , Api/User/Update : Admin Kullanıcıları güncelleyebilir ve silebilir

6-	 /api/User/GetRegularPayingUsers : Admin ödemelerini düzenli yapan user listi görebilir

7-	/api/Building/CreateBuilding : Admin kullanıcının dairelerin bulunduğu binayı oluşturması için

8-	/api/Apartment/CreateApartment : Admin Kullanıcısı bina id’siyle daire oluşturabilir

9-	/api/Apartment/AddRelationship : Admin Daireye bağlı kullanıcıları ayarlayabilir


10-	/api/Payment/AddSubscription : Admin bir daireye aidat ödemesi oluşturabilir

11-	/api/Payment/AddBills : Admin bir bina için doğalgaz, elektrik ve su faturası oluşturabilir. Faturalar binadaki tüm dairelere eşit şekilde bölünür.

12-	/api/Payment/AllApartmentsPayments : admin ödenmiş ve ödenmemiş tüm fatura ve aidatları görebilir

13-	/api/Payment/UserUnpaidPaymentsById/{id} : admin bir kullanıcının ödenmemiş tüm fatura ve aidatlarını görebilir

14-	/api/Payment/PaymentsbyMonthYearById/{Year}/{Month}/{apartmentId} : admin belirli bir yıl ve ay’da ki o dairenin fatura ve aidatlarnı görebilir

15-	/api/Payment/BuildingPaymentsById/{buildingId} : Admin binaya ait tüm faturaları görebilir

Residance User

1-	/api/User/SignIn : Giriş yaparak token alabilir

2-	/api/Payment/UserUnpaidPaymentsById/{id} : Residance kullanıcı kendi ödenmemiş fatura ve aidatlarını görebilir.

3-	/api/Payment/UserPaidPaymentsById/{id} : Residance kullanıcı kendi ödenmiş fatura ve aidatlarını görebilir

4-	/api/Payment/PayPayment : Residance kullanıcı bir faturasını veya aidatını ödeyebilir. Eğer ödemenin ilgili ay’ı ve yılında ödemezse %10 fazla tahsilat edilir. Ya da Kullanıcı ödemelerini bir önceki yıl düzenli ödemişse %10 indirim uygulanır.  (hesaplama PaymentHelper.cs ‘da bulunmaktadır. ) 
