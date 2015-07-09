----------------------------------- 1. Installation
1.1. Setup compatibility mode to Windows XP
1.2. Run Vissim.exe with -Automation parameter

----------------------------------- 2. Assumptions
2.1. Application starts when Vissim has been opened
2.2. Vissim running in -Automation mode (see 2.1.)
2.3. Сохраняются только файлы с расширением. Подкаталоги игнорируются
2.4. Никаких сохранений и загрузок, пока идёт эксеримент

------------------------------------3. Next steps
3.1. Save project files to DB
3.2. Implement interfaces for mode properties changing
		Состав потока в точках входа в модель
			static assignment - veh.compositions window
			dynamic assignment - veh.compositions window			
		Интенсивность потока в точках входа (vehicles per hour)
			static assignment - veh.inputs window
			dynamic assignment - OD
		+Скорости
			static assignment - veh.compositions window
			dynamic assignment - parking lots
		Процентное распределение трафика на перекрёстках
		Указанные маршруты
		Направления потоков
		Разрешённые повороты
		Разрешённая скорость следования
		OD матрица(ы)
		Параметры динамической маршрутизации
		Циклы горения светофоров
3.3. MultiRun
3.4. Get datatables names
3.5. Setup evaluations on/off

loose - C:\Program Files\PTV_Vision\VISSIM530\Exe\7za.exe

------------------------------------4. VISSIM BUGS
1. v.DisableToolbar(); -> Click Zoom = toolbar enabled
2. v.SaveLayout(@"D:\MOPO3OB\Desktop\BW\vissim2.ini"); -> throws exception (?)
3. return (string)eval.get_AttValue("TABLENAME");