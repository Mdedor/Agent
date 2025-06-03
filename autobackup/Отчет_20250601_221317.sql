DROP TABLE IF EXISTS applicant;
CREATE TABLE `applicant` (
  `applicant_id` int NOT NULL AUTO_INCREMENT,
  `applicant_surname` varchar(45) NOT NULL,
  `applicant_name` varchar(45) NOT NULL,
  `applicant_patronymic` varchar(45) NOT NULL,
  `applicant_phone_number` varchar(18) NOT NULL,
  `applicant_address` varchar(45) NOT NULL,
  `applicant_date_of_birth` date NOT NULL,
  `applicant_image` varchar(100) DEFAULT NULL,
  `applicant_gender` int NOT NULL,
  `applicant_delete_status` int DEFAULT NULL,
  PRIMARY KEY (`applicant_id`),
  KEY `fk_applicant_gender1_idx` (`applicant_gender`),
  KEY `applicant_delete_status_idx` (`applicant_delete_status`),
  CONSTRAINT `applicant_delete_status` FOREIGN KEY (`applicant_delete_status`) REFERENCES `delete` (`id`),
  CONSTRAINT `fk_applicant_gender1` FOREIGN KEY (`applicant_gender`) REFERENCES `gender` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=66 DEFAULT CHARSET=utf8mb3;

-- Data for table applicant

INSERT INTO `applicant` VALUES ('1', 'Иванов', 'Илья', 'Иванович', '+7 (900) 123-45-67', 'г. Москва, ул. Ленина, д. 5', '01.01.1990 0:00:00', '', '1', '4');
INSERT INTO `applicant` VALUES ('2', 'Петрова', 'Анна', 'Сергеевна', '+7 (900) 234-56-78', 'г. Санкт-Петербург, ул. Пушкина, д. 2', '02.02.1985 0:00:00', '', '2', NULL);
INSERT INTO `applicant` VALUES ('3', 'Сидорова', 'Алексей', 'Александрович', '+7 (900) 345-67-89', 'г. Казань, ул. Набережная, д. 3', '03.03.1992 0:00:00', '', '2', NULL);
INSERT INTO `applicant` VALUES ('4', 'Кузнецова', 'Мария', 'Викторовна', '+7 (900) 456-78-90', 'г. Екатеринбург, ул. Мира, д. 4', '04.04.1995 0:00:00', '', '2', '4');
INSERT INTO `applicant` VALUES ('5', 'Смирнов', 'Дмитрий', 'Дмитриевич', '+7 (900) 567-89-01', 'г. Новосибирск, ул. Степная, д. 5', '05.05.1988 0:00:00', '', '1', NULL);
INSERT INTO `applicant` VALUES ('6', 'Федорова', 'Елена', 'Петровна', '+7 (900) 678-90-12', 'г. Нижний Новгород, ул. Свободы, д. 6', '06.06.1993 0:00:00', '', '2', '3');
INSERT INTO `applicant` VALUES ('7', 'Морозов', 'Сергей', 'Игоревич', '+7 (900) 789-01-23', 'г. Челябинск, ул. Солнечная, д. 7', '07.07.1987 0:00:00', '', '1', '3');
INSERT INTO `applicant` VALUES ('8', 'Николаева', 'Ольга', 'Станиславовна', '+7 (900) 890-12-34', 'г. Омск, ул. Лесная, д. 8', '08.08.1991 0:00:00', '', '2', '3');
INSERT INTO `applicant` VALUES ('9', 'Алексеев', 'Владимир', 'Сергеевич', '+7 (900) 901-23-45', 'г. Ростов-на-Дону, ул. Центральная, д. 9', '09.09.1986 0:00:00', '', '1', NULL);
INSERT INTO `applicant` VALUES ('10', 'Соловьева', 'Татьяна', 'Анатольевна', '+7 (900) 012-34-56', 'г. Владивосток, ул. Морская, д. 10', '10.10.1994 0:00:00', '', '2', NULL);
INSERT INTO `applicant` VALUES ('11', 'Зайцева', 'Ксения', 'Ивановна', '+7 (900) 111-22-33', 'г. Саратов, ул. Октябрьская, д. 11', '11.11.1989 0:00:00', '', '2', NULL);
INSERT INTO `applicant` VALUES ('12', 'Семенов', 'Анатолий', 'Семенович', '+7 (900) 222-33-44', 'г. Уфа, ул. Гагарина, д. 12', '12.12.1984 0:00:00', '', '1', NULL);
INSERT INTO `applicant` VALUES ('13', 'Ковалев', 'Виктор', 'Петрович', '+7 (900) 333-44-55', 'г. Тюмень, ул. Лесная, д. 13', '13.01.1992 0:00:00', '', '1', NULL);
INSERT INTO `applicant` VALUES ('14', 'Григорьева', 'Наталья', 'Алексеевна', '+7 (900) 444-55-66', 'г. Чебоксары, ул. Спортивная, д. 14', '14.02.1987 0:00:00', '', '2', NULL);
INSERT INTO `applicant` VALUES ('15', 'Тихонов', 'Денис', 'Андреевич', '+7 (900) 555-66-77', 'г. Ижевск, ул. Пушкина, д. 15', '15.03.1990 0:00:00', '', '1', NULL);
INSERT INTO `applicant` VALUES ('16', 'Лебедева', 'Екатерина', 'Сергеевна', '+7 (900) 666-77-88', 'г. Барнаул, ул. Молодежная, д. 16', '16.04.1995 0:00:00', '', '2', NULL);
INSERT INTO `applicant` VALUES ('17', 'Кириллова', 'Ольга', 'Викторовна', '+7 (900) 777-88-99', 'г. Ярославль, ул. Ленина, д. 17', '17.05.1988 0:00:00', '', '2', NULL);
INSERT INTO `applicant` VALUES ('18', 'Соловьев', 'Максим', 'Анатольевич', '+7 (900) 888-99-00', 'г. Смоленск, ул. Солнечная, д. 18', '18.06.1991 0:00:00', '', '1', NULL);
INSERT INTO `applicant` VALUES ('19', 'Кузнецова', 'Татьяна', 'Игоревна', '+7 (900) 999-00-11', 'г. Тула, ул. Мира, д. 19', '19.07.1986 0:00:00', '', '2', NULL);
INSERT INTO `applicant` VALUES ('20', 'Михайлов', 'Роман', 'Владимирович', '+7 (900) 000-11-22', 'г. Воронеж, ул. Спортивная, д. 20', '20.08.1993 0:00:00', '', '1', NULL);
INSERT INTO `applicant` VALUES ('21', 'Борисова', 'Елена', 'Анатольевна', '+7 (900) 111-22-33', 'г. Пермь, ул. Набережная, д. 21', '21.09.1989 0:00:00', '', '2', NULL);
INSERT INTO `applicant` VALUES ('22', 'Громов', 'Игорь', 'Станиславович', '+7 (900) 222-33-44', 'г. Сургут, ул. Лесная, д. 22', '22.10.1984 0:00:00', '', '1', NULL);
INSERT INTO `applicant` VALUES ('23', 'Панина', 'Светлана', 'Игоревна', '+7 (900) 333-44-55', 'г. Набережные Челны, ул. Солнечная, д. 23', '23.11.1992 0:00:00', '', '2', NULL);
INSERT INTO `applicant` VALUES ('24', 'Терентьев', 'Александр', 'Сергеевич', '+7 (900) 444-55-66', 'г. Хабаровск, ул. Центральная, д. 24', '24.12.1987 0:00:00', '', '1', NULL);
INSERT INTO `applicant` VALUES ('25', 'Алексеева', 'Анна', 'Дмитриевна', '+7 (900) 555-66-77', 'г. Ставрополь, ул. Октябрьская, д. 25', '25.01.1990 0:00:00', '', '2', NULL);
INSERT INTO `applicant` VALUES ('26', 'Фролов', 'Дмитрий', 'Александрович', '+7 (900) 666-77-88', 'г. Петрозаводск, ул. Пушкина, д. 26', '26.02.1995 0:00:00', '', '1', NULL);
INSERT INTO `applicant` VALUES ('27', 'Семенова', 'Кристина', 'Станиславовна', '+7 (900) 777-88-99', 'г. Тверь, ул. Набережная, д. 27', '27.03.1988 0:00:00', '', '2', NULL);
INSERT INTO `applicant` VALUES ('28', 'Дмитриев', 'Станислав', 'Валерьевич', '+7 (900) 888-99-00', 'г. Липецк, ул. Молодежная, д. 28', '28.04.1991 0:00:00', '', '1', NULL);
INSERT INTO `applicant` VALUES ('29', 'Егорова', 'Марина', 'Алексеевна', '+7 (900) 999-00-11', 'г. Кострома, ул. Ленина, д. 29', '29.05.1986 0:00:00', '', '2', NULL);
INSERT INTO `applicant` VALUES ('30', 'Петров', 'Никита', 'Сергеевич', '+7 (900) 000-11-22', 'г. Архангельск, ул. Спортивная, д. 30', '30.06.1993 0:00:00', '', '1', NULL);
INSERT INTO `applicant` VALUES ('31', 'Сидорова', 'Виктория', 'Николаевна', '+7 (900) 111-22-33', 'г. Калуга, ул. Набережная, д. 31', '31.07.1989 0:00:00', '', '2', NULL);
INSERT INTO `applicant` VALUES ('32', 'Белов', 'Денис', 'Валентинович', '+7 (900) 222-33-44', 'г. Брянск, ул. Лесная, д. 32', '01.08.1984 0:00:00', '', '1', NULL);
INSERT INTO `applicant` VALUES ('33', 'Смирнова', 'Оксана', 'Станиславовна', '+7 (900) 333-44-55', 'г. Курск, ул. Молодежная, д. 33', '02.09.1992 0:00:00', '', '2', NULL);
INSERT INTO `applicant` VALUES ('34', 'Никитин', 'Евгений', 'Александрович', '+7 (900) 444-55-66', 'г. Сочи, ул. Центральная, д. 34', '03.10.1987 0:00:00', '', '1', NULL);
INSERT INTO `applicant` VALUES ('35', 'Лебедев', 'Светлана', 'Игоревна', '+7 (900) 555-66-77', 'г. Симферополь, ул. Ленина, д. 35', '04.11.1990 0:00:00', '', '2', NULL);
INSERT INTO `applicant` VALUES ('36', 'Морозов', 'Алексей', 'Дмитриевич', '+7 (900) 666-77-88', 'г. Анапа, ул. Пушкина, д. 36', '05.12.1995 0:00:00', '', '1', NULL);
INSERT INTO `applicant` VALUES ('37', 'Тихонова', 'Людмила', 'Васильевна', '+7 (900) 777-88-99', 'г. Владивосток, ул. Солнечная, д. 37', '06.01.1988 0:00:00', '', '2', NULL);
INSERT INTO `applicant` VALUES ('38', 'Соловьев', 'Григорий', 'Станиславович', '+7 (900) 888-99-00', 'г. Челябинск, ул. Спортивная, д. 38', '07.02.1991 0:00:00', '', '1', NULL);
INSERT INTO `applicant` VALUES ('39', 'Александрова', 'Дарья', 'Анатольевна', '+7 (900) 999-00-11', 'г. Ростов-на-Дону, ул. Молодежная, д. 39', '08.03.1986 0:00:00', '', '2', NULL);
INSERT INTO `applicant` VALUES ('40', 'Ковалев', 'Сергей', 'Александрович', '+7 (900) 000-11-22', 'г. Тула, ул. Набережная, д. 40', '09.04.1993 0:00:00', '', '1', NULL);
INSERT INTO `applicant` VALUES ('41', 'Федорова', 'Ирина', 'Викторовна', '+7 (900) 111-22-33', 'г. Нижний Новгород, ул. Лесная, д. 41', '10.05.1985 0:00:00', '', '2', NULL);
INSERT INTO `applicant` VALUES ('42', 'Смирнов', 'Александр', 'Валерьевич', '+7 (900) 222-33-44', 'г. Казань, ул. Центральная, д. 42', '11.06.1990 0:00:00', '', '1', NULL);
INSERT INTO `applicant` VALUES ('43', 'Николаева', 'Елена', 'Станиславовна', '+7 (900) 333-44-55', 'г. Омск, ул. Молодежная, д. 43', '12.07.1988 0:00:00', '', '2', NULL);
INSERT INTO `applicant` VALUES ('44', 'Павлов', 'Дмитрий', 'Анатольевич', '+7 (900) 444-55-66', 'г. Новосибирск, ул. Пушкина, д. 44', '13.08.1992 0:00:00', '', '1', NULL);
INSERT INTO `applicant` VALUES ('45', 'Гусева', 'Анна', 'Игоревна', '+7 (900) 555-66-77', 'г. Челябинск, ул. Набережная, д. 45', '14.09.1986 0:00:00', '', '2', NULL);
INSERT INTO `applicant` VALUES ('46', 'Кузьмин', 'Сергей', 'Дмитриевич', '+7 (900) 666-77-88', 'г. Самара, ул. Спортивная, д. 46', '15.10.1993 0:00:00', '', '1', NULL);
INSERT INTO `applicant` VALUES ('47', 'Мартынова', 'Татьяна', 'Сергеевна', '+7 (900) 777-88-99', 'г. Воронеж, ул. Октябрьская, д. 47', '16.11.1989 0:00:00', '', '2', NULL);
INSERT INTO `applicant` VALUES ('48', 'Савельев', 'Игорь', 'Александрович', '+7 (900) 888-99-00', 'г. Ростов-на-Дону, ул. Ленина, д. 48', '17.12.1991 0:00:00', '', '1', NULL);
INSERT INTO `applicant` VALUES ('49', 'Коваленко', 'Светлана', 'Петровна', '+7 (900) 999-00-11', 'г. Краснодар, ул. Молодежная, д. 49', '18.01.1987 0:00:00', '', '2', NULL);
INSERT INTO `applicant` VALUES ('50', 'Терентьев', 'Анатолий', 'Станиславович', '+7 (900) 000-11-22', 'г. Ижевск, ул. Солнечная, д. 50', '19.02.1995 0:00:00', '', '1', NULL);
INSERT INTO `applicant` VALUES ('51', 'уйцуй', 'уцйуйцу', 'уйцуйцу', '+7 (212) 121-2122', 'ццйц', '27.12.2005 0:00:00', '', '2', NULL);
INSERT INTO `applicant` VALUES ('52', 'уцйуй', 'уйцуйуй', 'уйцуй', '+7 (312) 313-13-21', 'уйцуйцуй', '01.01.2006 0:00:00', '', '1', NULL);
INSERT INTO `applicant` VALUES ('53', 'ц', 'ц', 'ц', '+7 (312) 312-33-13', 'уйцу', '01.01.2006 0:00:00', '', '1', NULL);
INSERT INTO `applicant` VALUES ('54', 'уйцуйцу', 'уйцуйцу', 'уйцуйц', '+7 (312) 313-21-23', 'уйцуйуйцу', '01.01.2006 0:00:00', '', '1', NULL);
INSERT INTO `applicant` VALUES ('55', 'йуйцуй', 'йцуйцуйцуцуйцу', 'уйцуйцу', '+7 (313) 123-12-31', 'йцуйцуйцуйцуйцуй', '01.01.2006 0:00:00', '', '1', NULL);
INSERT INTO `applicant` VALUES ('56', 'кцукцук', 'кцук', 'куцкцук', '+7 (433) 423-42-34', 'уйцуйуйцу', '01.01.2006 0:00:00', '', '1', NULL);
INSERT INTO `applicant` VALUES ('57', 'уйцуй', 'уцйуйц', 'уйцу', '+7 (312) 312-31-23', 'уйцуйцу', '01.01.2006 0:00:00', '', '1', NULL);
INSERT INTO `applicant` VALUES ('58', 'уйцуй', 'уйцу', 'уйцу', '+7 (312) 312-31-32', 'уйцуйуцу', '01.01.2006 0:00:00', '', '1', NULL);
INSERT INTO `applicant` VALUES ('59', 'йцу', 'уйц', 'уцй', '+7 (312) 312-31-12', 'уйцу', '01.01.2006 0:00:00', '', '1', NULL);
INSERT INTO `applicant` VALUES ('60', 'Уйцу', 'Йуцйцу', 'Цуйуц', '+7 (313) 212-31-23', 'уйцуйц', '01.01.2006 0:00:00', '', '2', NULL);
INSERT INTO `applicant` VALUES ('61', 'Цуйцуйцу', 'Уйцуйцуцу', 'Уйцуйцу', '+7 (123) 123-11-23', 'цуйцуйцуйц', '01.01.2006 0:00:00', '$2a$11$FL9ZUtKRSM2V07oZc9ot2eIoPtGdtpHMaHfiyTYr6hvY0PRInBrPu.jpg', '1', NULL);
INSERT INTO `applicant` VALUES ('62', 'Ива', 'Ива', 'Ива', '+7 (312) 321-31-23', 'уйцуйцу', '01.01.2006 0:00:00', '', '1', NULL);
INSERT INTO `applicant` VALUES ('63', 'Ива', 'Ива', 'Ива', '+7 (312) 321-31-21', 'уйцуйцу', '01.01.2006 0:00:00', '', '1', NULL);
INSERT INTO `applicant` VALUES ('64', 'Ц', 'Ц', 'Ц', '+7 (111) 111-11-11', 'ц', '01.01.2006 0:00:00', '', '1', NULL);
INSERT INTO `applicant` VALUES ('65', 'Низов', 'Егор', 'Александрович', '+7 (323) 123-12-33', 'г. Заволжье', '01.01.2006 0:00:00', '', '1', NULL);

DROP TABLE IF EXISTS company;
CREATE TABLE `company` (
  `id` int NOT NULL AUTO_INCREMENT,
  `company_name` varchar(100) NOT NULL,
  `company_desceiption` varchar(1000) NOT NULL,
  `company_phone_number` varchar(18) NOT NULL,
  `company_address` varchar(45) NOT NULL,
  `companyc_linq` varchar(200) DEFAULT NULL,
  `company_delete_status` int DEFAULT NULL,
  `company_vacancy_cost` int NOT NULL DEFAULT '0',
  PRIMARY KEY (`id`),
  KEY `company_delete_status_idx` (`company_delete_status`),
  CONSTRAINT `company_delete_status` FOREIGN KEY (`company_delete_status`) REFERENCES `delete` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=56 DEFAULT CHARSET=utf8mb3;

-- Data for table company

INSERT INTO `company` VALUES ('1', 'ТехноСервис', 'Компания, занимающаяся ремонтом и обслуживанием техники', '+7 (495) 123-45-67', 'г. Москва, ул. Техническая, д. 1', '', NULL, '2');
INSERT INTO `company` VALUES ('2', 'ЭкоПродукты', 'Производитель органических продуктов питания', '+7 (495) 234-56-78', 'г. Санкт-Петербург, ул. Эко, д. 2', 'https://ya.ru/', NULL, '222222');
INSERT INTO `company` VALUES ('3', 'СтройГрупп', 'Строительная компания, специализирующаяся на жилых и коммерческих объектах', '+7 (495) 345-67-89', 'г. Казань, ул. Строителей, д. 3', 'https://ya.ru/', NULL, '0');
INSERT INTO `company` VALUES ('4', 'IT-Решения', 'Компания, предлагающая IT-услуги и разработки программного обеспечения', '+7 (495) 456-78-90', 'г. Новосибирск, ул. Инноваций, д. 4', 'https://ya.ru/', NULL, '0');
INSERT INTO `company` VALUES ('5', 'АвтоМир', 'Сервисный центр по ремонту автомобилей', '+7 (495) 567-89-01', 'г. Екатеринбург, ул. Автомобильная, д. 5', 'https://ya.ru/', NULL, '0');
INSERT INTO `company` VALUES ('6', 'ФинансГрупп', 'Финансовая компания, предоставляющая услуги кредитования', '+7 (495) 678-90-12', 'г. Нижний Новгород, ул. Финансовая, д. 6', 'https://ya.ru/', NULL, '0');
INSERT INTO `company` VALUES ('7', 'Логистика24', 'Компания, занимающаяся грузоперевозками и логистическими услугами', '+7 (495) 789-01-23', 'г. Челябинск, ул. Логистическая, д. 7', 'https://ya.ru/', NULL, '0');
INSERT INTO `company` VALUES ('8', 'КлиматСистемы', 'Производитель систем кондиционирования и вентиляции', '+7 (495) 890-12-34', 'г. Ростов-на-Дону, ул. Климатическая, д. 8', 'https://ya.ru/', NULL, '0');
INSERT INTO `company` VALUES ('9', 'КреативСтудия', 'Рекламное агентство, предоставляющее услуги по созданию брендов', '+7 (495) 901-23-45', 'г. Уфа, ул. Креативная, д. 9', 'https://ya.ru/', NULL, '0');
INSERT INTO `company` VALUES ('10', 'МедТех', 'Компания, занимающаяся продажей медицинского оборудования', '+7 (495) 012-34-56', 'г. Самара, ул. Медицинская, д. 10', 'https://ya.ru/', NULL, '0');
INSERT INTO `company` VALUES ('11', 'СтильДекор', 'Дизайнерская компания, специализирующаяся на интерьере', '+7 (495) 123-45-67', 'г. Волгоград, ул. Декоративная, д. 11', 'https://ya.ru/', NULL, '0');
INSERT INTO `company` VALUES ('12', 'Энергия', 'Энергетическая компания, предоставляющая услуги электроснабжения', '+7 (495) 234-56-78', 'г. Тюмень, ул. Энергетическая, д. 12', 'https://ya.ru/', NULL, '0');
INSERT INTO `company` VALUES ('13', 'АгроФирма', 'Компания, занимающаяся производством и продажей сельскохозяйственной продукции', '+7 (495) 345-67-89', 'г. Краснодар, ул. Сельская, д. 13', 'https://ya.ru/', NULL, '0');
INSERT INTO `company` VALUES ('14', 'СтройПартнер', 'Строительная компания, предлагающая услуги по реконструкции', '+7 (495) 456-78-90', 'г. Сочи, ул. Партнерская, д. 14', 'https://ya.ru/', NULL, '0');
INSERT INTO `company` VALUES ('15', 'ТуризмПлюс', 'Туристическое агентство, предлагающее экскурсии и туры', '+7 (495) 567-89-01', 'г. Владивосток, ул. Туристическая, д. 15', 'https://ya.ru/', NULL, '0');
INSERT INTO `company` VALUES ('16', 'Здоровье', 'Медицинский центр, предоставляющий услуги диагностики и лечения', '+7 (495) 678-90-12', 'г. Челябинск, ул. Здоровья, д. 16', 'https://ya.ru/', NULL, '0');
INSERT INTO `company` VALUES ('17', 'IT-Глобал', 'IT-компания, занимающаяся разработкой программного обеспечения', '+7 (495) 789-01-23', 'г. Казань, ул. Глобальная, д. 17', 'https://ya.ru/', NULL, '0');
INSERT INTO `company` VALUES ('18', 'Кулинария', 'Кулинарная школа, предлагающая курсы по кулинарии', '+7 (495) 890-12-34', 'г. Омск, ул. Кулинарная, д. 18', 'https://ya.ru/', NULL, '0');
INSERT INTO `company` VALUES ('19', 'ТехноПром', 'Компания, занимающаяся производством промышленных товаров', '+7 (495) 901-23-45', 'г. Саратов, ул. Промышленная, д. 19', 'https://ya.ru/', NULL, '0');
INSERT INTO `company` VALUES ('20', 'ФинансСервис', 'Финансовая компания, предоставляющая бухгалтерские услуги', '+7 (495) 012-34-56', 'г. Ярославль, ул. Финансовая, д. 20', 'https://ya.ru/', NULL, '0');
INSERT INTO `company` VALUES ('21', 'Гармония', 'Центр wellness и фитнеса', '+7 (495) 123-45-67', 'г. Тула, ул. Гармония, д. 21', 'https://ya.ru/', NULL, '0');
INSERT INTO `company` VALUES ('22', 'ЭкоТехнологии', 'Компания, разрабатывающая экологически чистые технологии', '+7 (495) 234-56-78', 'г. Псков, ул. Эко, д. 22', 'https://ya.ru/', NULL, '0');
INSERT INTO `company` VALUES ('23', 'ТекстильПлюс', 'Производитель текстильных изделий', '+7 (495) 345-67-89', 'г. Иваново, ул. Текстильная, д. 23', 'https://ya.ru/', NULL, '0');
INSERT INTO `company` VALUES ('24', 'СтройКомфорт', 'Компания, занимающаяся строительством и дизайном интерьеров', '+7 (495) 456-78-90', 'г. Архангельск, ул. Комфортная, д. 24', 'https://ya.ru/', NULL, '0');
INSERT INTO `company` VALUES ('25', 'Косметика', 'Производитель косметических средств', '+7 (495) 567-89-01', 'г. Рязань, ул. Косметическая, д. 25', 'https://ya.ru/', NULL, '0');
INSERT INTO `company` VALUES ('26', 'ТранспортГрупп', 'Логистическая компания, предоставляющая услуги грузоперевозок', '+7 (495) 678-90-12', 'г. Курск, ул. Транспортная, д. 26', 'https://ya.ru/', NULL, '0');
INSERT INTO `company` VALUES ('27', 'АгроТех', 'Компания, занимающаяся продажей сельскохозяйственной техники', '+7 (495) 789-01-23', 'г. Воронеж, ул. Техническая, д. 27', 'https://ya.ru/', NULL, '0');
INSERT INTO `company` VALUES ('28', 'Строительные Решения', 'Компания, предлагающая комплексные строительные решения', '+7 (495) 890-12-34', 'г. Тверь, ул. Строительная, д. 28', 'https://ya.ru/', NULL, '0');
INSERT INTO `company` VALUES ('29', 'ДизайнСтудия', 'Дизайнерская студия, специализирующаяся на графическом дизайне', '+7 (495) 901-23-45', 'г. Кострома, ул. Дизайнерская, д. 29', 'https://ya.ru/', NULL, '0');
INSERT INTO `company` VALUES ('30', 'КлиматТех', 'Компания по продаже климатического оборудования', '+7 (495) 012-34-56', 'г. Набережные Челны, ул. Климатическая, д. 30', 'https://ya.ru/', NULL, '0');
INSERT INTO `company` VALUES ('31', 'Финансовые Услуги', 'Компания, предоставляющая услуги по финансовому консультированию', '+7 (495) 123-45-67', 'г. Ставрополь, ул. Финансовая, д. 31', 'https://ya.ru/', NULL, '0');
INSERT INTO `company` VALUES ('32', 'Креативная Мастерская', 'Студия, занимающаяся созданием уникальных изделий ручной работы', '+7 (495) 234-56-78', 'г. Смоленск, ул. Мастерская, д. 32', 'https://ya.ru/', NULL, '0');
INSERT INTO `company` VALUES ('33', 'Сеть Ресторанов', 'Сеть ресторанов с разнообразным меню', '+7 (495) 345-67-89', 'г. Таганрог, ул. Ресторанная, д. 33', 'https://ya.ru/', NULL, '0');
INSERT INTO `company` VALUES ('34', 'Гостиница', 'Гостиница, предлагающая комфортные номера для отдыха', '+7 (495) 456-78-90', 'г. Анапа, ул. Гостиничная, д. 34', 'https://ya.ru/', NULL, '0');
INSERT INTO `company` VALUES ('35', 'Туристическая Компания', 'Компания, предлагающая услуги по организации туров', '+7 (495) 567-89-01', 'г. Новороссийск, ул. Туристическая, д. 35', 'https://ya.ru/', NULL, '0');
INSERT INTO `company` VALUES ('36', 'Спортивный Клуб', 'Клуб, предлагающий занятия спортом и фитнесом', '+7 (495) 678-90-12', 'г. Сыктывкар, ул. Спортивная, д. 36', 'https://ya.ru/', NULL, '0');
INSERT INTO `company` VALUES ('37', 'Арт-Студия', 'Студия, занимающаяся художественным обучением и творческими проектами', '+7 (495) 789-01-23', 'г. Кемерово, ул. Арт, д. 37', 'https://ya.ru/', NULL, '0');
INSERT INTO `company` VALUES ('38', 'Курьерская Служба', 'Компания, предоставляющая услуги доставки', '+7 (495) 890-12-34', 'г. Улан-Удэ, ул. Курьерская, д. 38', 'https://ya.ru/', NULL, '0');
INSERT INTO `company` VALUES ('39', 'Школа Танцев', 'Школа, предлагающая занятия различными танцевальными направлениями', '+7 (495) 901-23-45', 'г. Хабаровск, ул. Танцевальная, д. 39', 'https://ya.ru/', NULL, '0');
INSERT INTO `company` VALUES ('40', 'Мастерская', 'Мастерская, предлагающая услуги по ремонту и изготовлению изделий', '+7 (495) 012-34-56', 'г. Чита, ул. Мастерская, д. 40', 'https://ya.ru/', NULL, '0');
INSERT INTO `company` VALUES ('41', 'Книжный Магазин', 'Магазин, предлагающий широкий ассортимент книг', '+7 (495) 123-45-67', 'г. Саранск, ул. Книжная, д. 41', 'https://ya.ru/', NULL, '0');
INSERT INTO `company` VALUES ('42', 'Салон Красоты', 'Салон, предлагающий услуги по уходу за внешностью', '+7 (495) 234-56-78', 'г. Махачкала, ул. Красоты, д. 42', 'https://ya.ru/', NULL, '0');
INSERT INTO `company` VALUES ('43', 'Кофейня', 'Кофейня, предлагающая разнообразные напитки и десерты', '+7 (495) 345-67-89', 'г. Грозный, ул. Кофейная, д. 43', 'https://ya.ru/', NULL, '0');
INSERT INTO `company` VALUES ('44', 'Студия Фотографии', 'Студия, предоставляющая услуги профессиональной фотосъемки', '+7 (495) 456-78-90', 'г. Нальчик, ул. Фотографическая, д. 44', 'https://ya.ru/', NULL, '0');
INSERT INTO `company` VALUES ('45', 'Служба Уборки', 'Компания, предоставляющая услуги по уборке помещений', '+7 (495) 567-89-01', 'г. Тверь, ул. Уборочная, д. 45', 'https://ya.ru/', NULL, '0');
INSERT INTO `company` VALUES ('46', 'Сеть Магазинов', 'Сеть магазинов, предлагающая товары повседневного спроса', '+7 (495) 678-90-12', 'г. Уфа, ул. Магазинная, д. 46', 'https://ya.ru/', NULL, '0');
INSERT INTO `company` VALUES ('47', 'Клуб Любителей Книг', 'Клуб, объединяющий любителей чтения', '+7 (495) 789-01-23', 'г. Тула, ул. Читательская, д. 47', 'https://ya.ru/', NULL, '0');
INSERT INTO `company` VALUES ('48', 'Фотостудия', 'Студия, предлагающая услуги по фотосъемке и видеосъемке', '+7 (495) 890-12-34', 'г. Сочи, ул. Фотостудийная, д. 48', 'https://ya.ru/', NULL, '0');
INSERT INTO `company` VALUES ('49', 'Студия Моды', 'Студия, занимающаяся дизайном одежды', '+7 (495) 901-23-45', 'г. Ростов-на-Дону, ул. Модная, д. 49', 'https://ya.ru/', NULL, '0');
INSERT INTO `company` VALUES ('50', 'Кулинарная Школа', 'Школа, предлагающая курсы по кулинарии и выпечке', '+7 (495) 012-34-56', 'г. Краснодар, ул. Кулинарная, д. 50', 'https://ya.ru/', NULL, '0');
INSERT INTO `company` VALUES ('51', 'dweqwe', 'wqeqweqwe', '+7 (311) 312-31-23', 'eqweqw', 'https://ya.ru/', NULL, '0');
INSERT INTO `company` VALUES ('52', 'wqwqrqwe', 'eqweqweqweqwe', '+7 (123) 212-31-23', 'weqweqweqwe', 'eqweqweqwe', NULL, '0');
INSERT INTO `company` VALUES ('53', '2eqe', 'eqweqwe', '+7 (123) 212-31-23', 'weqwe', 'eqweqwe', NULL, '0');
INSERT INTO `company` VALUES ('54', 'уйцуй', 'уйуйцу', '+7 (312) 312-31-23', 'уйцу', '', NULL, '0');
INSERT INTO `company` VALUES ('55', 'Яндекс', 'нет', '+7 (312) 331-23-12', 'г Заволжье', '', NULL, '0');

DROP TABLE IF EXISTS delete;
CREATE TABLE `delete` (
  `id` int NOT NULL AUTO_INCREMENT,
  `status` varchar(45) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb3;

-- Data for table delete

INSERT INTO `delete` VALUES ('1', 'Удален');
INSERT INTO `delete` VALUES ('2', 'Закрыта');
INSERT INTO `delete` VALUES ('3', 'Работает');
INSERT INTO `delete` VALUES ('4', 'Не удален');

DROP TABLE IF EXISTS direction;
CREATE TABLE `direction` (
  `id` int NOT NULL AUTO_INCREMENT,
  `direction_aplicant` int NOT NULL,
  `direction_vacancy` int NOT NULL,
  `direction_employee` int NOT NULL,
  `direction_date` date NOT NULL,
  `direction_status` varchar(45) NOT NULL,
  `direction_delete_status` int DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_direction_vacancy1_idx` (`direction_vacancy`),
  KEY `fk_direction_applicant1_idx` (`direction_aplicant`),
  KEY `direction_delete_status_idx` (`direction_delete_status`),
  KEY `direction_employee_idx` (`direction_employee`),
  CONSTRAINT `direction_delete_status` FOREIGN KEY (`direction_delete_status`) REFERENCES `delete` (`id`),
  CONSTRAINT `direction_employee` FOREIGN KEY (`direction_employee`) REFERENCES `employe` (`id`),
  CONSTRAINT `fk_direction_applicant1` FOREIGN KEY (`direction_aplicant`) REFERENCES `applicant` (`applicant_id`),
  CONSTRAINT `fk_direction_vacancy1` FOREIGN KEY (`direction_vacancy`) REFERENCES `vacancy` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=61 DEFAULT CHARSET=utf8mb3;

-- Data for table direction

INSERT INTO `direction` VALUES ('1', '1', '1', '1', '01.01.2023 0:00:00', 'Ожидание', NULL);
INSERT INTO `direction` VALUES ('2', '2', '2', '1', '02.01.2023 0:00:00', 'Принято', NULL);
INSERT INTO `direction` VALUES ('3', '3', '3', '1', '03.01.2023 0:00:00', 'Отклонено', NULL);
INSERT INTO `direction` VALUES ('4', '4', '4', '1', '04.01.2023 0:00:00', 'Ожидание', NULL);
INSERT INTO `direction` VALUES ('5', '5', '5', '1', '05.01.2023 0:00:00', 'Принято', NULL);
INSERT INTO `direction` VALUES ('6', '6', '6', '1', '06.01.2023 0:00:00', 'Отклонено', NULL);
INSERT INTO `direction` VALUES ('7', '7', '7', '1', '07.01.2023 0:00:00', 'Ожидание', NULL);
INSERT INTO `direction` VALUES ('8', '8', '8', '1', '08.01.2023 0:00:00', 'Принято', NULL);
INSERT INTO `direction` VALUES ('9', '9', '9', '1', '09.01.2023 0:00:00', 'Отклонено', NULL);
INSERT INTO `direction` VALUES ('10', '10', '10', '1', '10.01.2023 0:00:00', 'Ожидание', NULL);
INSERT INTO `direction` VALUES ('11', '11', '11', '1', '11.01.2023 0:00:00', 'Принято', NULL);
INSERT INTO `direction` VALUES ('12', '12', '12', '1', '12.01.2023 0:00:00', 'Отклонено', NULL);
INSERT INTO `direction` VALUES ('13', '13', '13', '1', '13.01.2023 0:00:00', 'Ожидание', NULL);
INSERT INTO `direction` VALUES ('14', '14', '14', '1', '14.01.2023 0:00:00', 'Принято', NULL);
INSERT INTO `direction` VALUES ('15', '15', '15', '1', '15.01.2023 0:00:00', 'Отклонено', NULL);
INSERT INTO `direction` VALUES ('16', '16', '16', '1', '16.01.2023 0:00:00', 'Ожидание', NULL);
INSERT INTO `direction` VALUES ('17', '17', '17', '1', '17.01.2023 0:00:00', 'Принято', NULL);
INSERT INTO `direction` VALUES ('18', '18', '18', '1', '18.01.2023 0:00:00', 'Отклонено', NULL);
INSERT INTO `direction` VALUES ('19', '19', '19', '1', '19.01.2023 0:00:00', 'Ожидание', NULL);
INSERT INTO `direction` VALUES ('20', '20', '20', '1', '20.01.2023 0:00:00', 'Принято', NULL);
INSERT INTO `direction` VALUES ('21', '21', '21', '1', '21.01.2023 0:00:00', 'Отклонено', NULL);
INSERT INTO `direction` VALUES ('22', '22', '22', '1', '22.01.2023 0:00:00', 'Ожидание', NULL);
INSERT INTO `direction` VALUES ('23', '23', '23', '1', '23.01.2023 0:00:00', 'Принято', NULL);
INSERT INTO `direction` VALUES ('24', '24', '24', '1', '24.01.2023 0:00:00', 'Отклонено', NULL);
INSERT INTO `direction` VALUES ('25', '25', '25', '1', '25.01.2023 0:00:00', 'Ожидание', NULL);
INSERT INTO `direction` VALUES ('26', '26', '26', '1', '26.01.2023 0:00:00', 'Принято', NULL);
INSERT INTO `direction` VALUES ('27', '27', '27', '1', '27.01.2023 0:00:00', 'Отклонено', NULL);
INSERT INTO `direction` VALUES ('28', '28', '28', '1', '28.01.2023 0:00:00', 'Ожидание', NULL);
INSERT INTO `direction` VALUES ('29', '29', '29', '1', '29.01.2023 0:00:00', 'Принято', NULL);
INSERT INTO `direction` VALUES ('30', '30', '30', '1', '30.01.2023 0:00:00', 'Отклонено', NULL);
INSERT INTO `direction` VALUES ('31', '31', '31', '1', '31.01.2023 0:00:00', 'Ожидание', NULL);
INSERT INTO `direction` VALUES ('32', '32', '32', '1', '01.02.2023 0:00:00', 'Принято', NULL);
INSERT INTO `direction` VALUES ('33', '33', '33', '1', '02.02.2023 0:00:00', 'Отклонено', NULL);
INSERT INTO `direction` VALUES ('34', '34', '34', '1', '03.02.2023 0:00:00', 'Ожидание', NULL);
INSERT INTO `direction` VALUES ('35', '35', '35', '1', '04.02.2023 0:00:00', 'Принято', NULL);
INSERT INTO `direction` VALUES ('36', '36', '36', '1', '05.02.2023 0:00:00', 'Отклонено', NULL);
INSERT INTO `direction` VALUES ('37', '37', '37', '1', '06.02.2023 0:00:00', 'Ожидание', NULL);
INSERT INTO `direction` VALUES ('38', '38', '38', '1', '07.02.2023 0:00:00', 'Принято', NULL);
INSERT INTO `direction` VALUES ('39', '39', '39', '1', '08.02.2023 0:00:00', 'Отклонено', NULL);
INSERT INTO `direction` VALUES ('40', '40', '40', '1', '09.02.2023 0:00:00', 'Ожидание', NULL);
INSERT INTO `direction` VALUES ('41', '41', '41', '1', '10.02.2023 0:00:00', 'Принято', NULL);
INSERT INTO `direction` VALUES ('42', '42', '42', '1', '11.02.2023 0:00:00', 'Отклонено', NULL);
INSERT INTO `direction` VALUES ('43', '43', '43', '1', '12.02.2023 0:00:00', 'Ожидание', NULL);
INSERT INTO `direction` VALUES ('44', '44', '44', '1', '13.02.2023 0:00:00', 'Принято', NULL);
INSERT INTO `direction` VALUES ('45', '45', '45', '1', '14.02.2023 0:00:00', 'Отклонено', NULL);
INSERT INTO `direction` VALUES ('46', '46', '46', '1', '15.02.2023 0:00:00', 'Ожидание', NULL);
INSERT INTO `direction` VALUES ('47', '47', '47', '1', '16.02.2023 0:00:00', 'Принято', NULL);
INSERT INTO `direction` VALUES ('48', '48', '48', '1', '17.02.2023 0:00:00', 'Отклонено', NULL);
INSERT INTO `direction` VALUES ('49', '49', '49', '1', '18.02.2023 0:00:00', 'Отклонено', NULL);
INSERT INTO `direction` VALUES ('50', '50', '50', '1', '19.02.2023 0:00:00', 'Принято', NULL);
INSERT INTO `direction` VALUES ('51', '40', '4', '1', '14.11.2024 0:00:00', 'Принято', NULL);
INSERT INTO `direction` VALUES ('52', '8', '5', '1', '14.11.2024 0:00:00', 'Отклонено', NULL);
INSERT INTO `direction` VALUES ('53', '4', '4', '1', '15.11.2024 0:00:00', 'Отклонено', NULL);
INSERT INTO `direction` VALUES ('54', '6', '6', '1', '16.11.2024 0:00:00', 'Принято', NULL);
INSERT INTO `direction` VALUES ('55', '20', '54', '2', '30.11.2024 0:00:00', 'Ожидание', NULL);
INSERT INTO `direction` VALUES ('56', '20', '51', '2', '30.11.2024 0:00:00', 'Ожидание', NULL);
INSERT INTO `direction` VALUES ('57', '20', '51', '2', '30.11.2024 0:00:00', 'Ожидание', NULL);
INSERT INTO `direction` VALUES ('58', '1', '51', '2', '01.12.2024 0:00:00', 'Отклонено', NULL);
INSERT INTO `direction` VALUES ('59', '1', '51', '2', '01.12.2024 0:00:00', 'Ожидание', NULL);
INSERT INTO `direction` VALUES ('60', '7', '9', '2', '03.12.2024 0:00:00', 'Принято', NULL);

DROP TABLE IF EXISTS employe;
CREATE TABLE `employe` (
  `id` int NOT NULL AUTO_INCREMENT,
  `employe_surname` varchar(45) NOT NULL,
  `employe_name` varchar(45) NOT NULL,
  `employe_partronymic` varchar(45) NOT NULL,
  `employe_phone_number` varchar(18) NOT NULL,
  `employe_adress` varchar(100) NOT NULL,
  `employe_login` varchar(45) NOT NULL,
  `employe_pwd` varchar(100) NOT NULL,
  `employe_post` int NOT NULL,
  `employe_delete_status` int DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_employee_post1_idx` (`employe_post`),
  KEY `employe_delete_status_idx` (`employe_delete_status`),
  CONSTRAINT `employe_delete_status` FOREIGN KEY (`employe_delete_status`) REFERENCES `delete` (`id`),
  CONSTRAINT `fk_employee_post1` FOREIGN KEY (`employe_post`) REFERENCES `post` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=19 DEFAULT CHARSET=utf8mb3;

-- Data for table employe

INSERT INTO `employe` VALUES ('1', 'Капотова', 'Мария', 'Игоревна', '+7 (910) 878-36-46', 'г.Заволжье, пр-кт Дзержинского, д43', '1', '$2a$12$D6a7stkrEOMLzhY6YAci9.X9W.zhcrRWzRN/j0Qv9Qc77n6WXfcSm', '1', '1');
INSERT INTO `employe` VALUES ('2', 'Низова', 'Ольга', 'Александровна', '+7 (999) 456-78-90', 'г.Заволжье, пр-кт Дзержинского, д43', '2', '$2a$12$OIXmjoCAXL/MRQ9ulr7VHORp7dCPScWU.OGTHO4bjaDu6rGqyXjO6', '2', '1');
INSERT INTO `employe` VALUES ('4', 'Кузнецова', 'Мария', 'Викторовна', '+7 (999) 456-78-90', 'г. Екатеринбург, ул. Чапаева, д. 4', 'mariaw', 'ma', '2', NULL);
INSERT INTO `employe` VALUES ('5', 'Смирнов', 'Алексей', 'Игоревич', '+7 (999) 567-89-01', 'г. Новосибирск, ул. Крылова, д. 5', 'smirnov_alexey', 'al', '1', NULL);
INSERT INTO `employe` VALUES ('6', 'Федорова', 'Елена', 'Анатольевна', '+7 (999) 678-90-12', 'г. Нижний Новгород, ул. Лермонтова, д. 6', 'fedorova_elena', 'el', '2', NULL);
INSERT INTO `employe` VALUES ('7', 'Морозов', 'Сергей', 'Валерьевич', '+7 (999) 789-01-23', 'г. Челябинск, ул. Транспортная, д. 7', 'morozov_sergey', 'se', '1', NULL);
INSERT INTO `employe` VALUES ('8', 'Соловьева', 'Ольга', 'Дмитриевна', '+7 (999) 890-12-34', 'г. Ростов-на-Дону, ул. Спортивная, д. 8', 'soloveva_olga', 'ol', '2', NULL);
INSERT INTO `employe` VALUES ('9', 'Васильев', 'Виктор', 'Николаевич', '+7 (999) 901-23-45', 'г. Уфа, ул. Центральная, д. 9', 'vasiliev_viktor', 'vi', '1', '1');
INSERT INTO `employe` VALUES ('10', 'Коваленко', 'Татьяна', 'Петровна', '+7 (999) 012-34-56', 'г. Саратов, ул. Новая, д. 10', 'kovalenko_tatyana', 'ta', '2', '1');
INSERT INTO `employe` VALUES ('13', 'Низов', 'Егор', 'Александрович', '+7 (910) 878-36-46', 'г.Заволжье, пр-кт Дзержинского, д43', 'admin', '$2a$11$AyGC3Xk5lWjlXpA7FgFS/.2oavmCXVFdFsdRNzicZ24sb/SuZ.MOq', '1', NULL);
INSERT INTO `employe` VALUES ('14', 'w', 'w', 'w', '+7 (312) 312-31-23', 'www', 'qq', '$2a$11$JwbvW/bjxWg8MT8TrkWGmes/1S1Typ.PqyG5mYQQn3ZjO8ApX8FQa', '1', '1');
INSERT INTO `employe` VALUES ('15', '3123', '321312', '31231', '+7 (312) 321-31-23', 'qeqw', 'asd', '$2a$11$txzAmhiF7QrgtQAprpvdle2zXs7o.9Gj1qVo2EBz8TSLNBHrc.cuW', '1', '1');
INSERT INTO `employe` VALUES ('16', 'уйцуйц', 'уйцу', 'уйцуйцу', '+7 (312) 312-31-33', 'уйцуйу', 'уйцуй', '$2a$11$nD25If.ii9i8Lrce4kO.jOf7B39DMtCpIicbV0IDa39y1fSgY40Ua', '1', NULL);
INSERT INTO `employe` VALUES ('17', 'Иванов', 'Иван', 'Иванович', '+7 (323) 123-21-33', 'г.Заволжье', 'iva', '$2a$11$FjHuazDgIdsneyAuVQOIt./YfoV5AbIrS2AKU9N1Sr3.ZhYUiPo6G', '1', NULL);
INSERT INTO `employe` VALUES ('18', 'Низов', 'Егор ', 'Александрович', '+7 (910) 878-36-46', 'г.Заволжье, пр-кт Дзержинского, д43', '3', '$2y$10$XD4qSHIT..hizxEJXVPKt.mmg6XgiMQpIpngcPIORVrrRYpUpW0QG', '3', NULL);

DROP TABLE IF EXISTS gender;
CREATE TABLE `gender` (
  `id` int NOT NULL,
  `genders` varchar(45) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

-- Data for table gender

INSERT INTO `gender` VALUES ('1', 'Мужской');
INSERT INTO `gender` VALUES ('2', 'Женский');

DROP TABLE IF EXISTS post;
CREATE TABLE `post` (
  `id` int NOT NULL AUTO_INCREMENT,
  `posts` varchar(45) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb3;

-- Data for table post

INSERT INTO `post` VALUES ('1', 'Администратор');
INSERT INTO `post` VALUES ('2', 'Менеджер');
INSERT INTO `post` VALUES ('3', 'Рекрутер');

DROP TABLE IF EXISTS profession;
CREATE TABLE `profession` (
  `id` int NOT NULL AUTO_INCREMENT,
  `name` varchar(45) NOT NULL,
  `description` varchar(150) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=56 DEFAULT CHARSET=utf8mb3;

-- Data for table profession

INSERT INTO `profession` VALUES ('1', 'HR-менеджер', NULL);
INSERT INTO `profession` VALUES ('2', 'UX/UI дизайнер', NULL);
INSERT INTO `profession` VALUES ('3', 'Аналитик', NULL);
INSERT INTO `profession` VALUES ('4', 'Аналитик данных', NULL);
INSERT INTO `profession` VALUES ('5', 'Ассистент', NULL);
INSERT INTO `profession` VALUES ('6', 'Ассистент маркетолога', NULL);
INSERT INTO `profession` VALUES ('7', 'Веб-разработчик', NULL);
INSERT INTO `profession` VALUES ('8', 'Графический дизайнер', NULL);
INSERT INTO `profession` VALUES ('9', 'Дизайнер', NULL);
INSERT INTO `profession` VALUES ('10', 'Инженер', NULL);
INSERT INTO `profession` VALUES ('11', 'Клиентский менеджер', NULL);
INSERT INTO `profession` VALUES ('12', 'Копирайтер', NULL);
INSERT INTO `profession` VALUES ('13', 'Маркетолог', NULL);
INSERT INTO `profession` VALUES ('14', 'Менеджер по качеству', NULL);
INSERT INTO `profession` VALUES ('15', 'Менеджер по продажам', NULL);
INSERT INTO `profession` VALUES ('16', 'Менеджер по продукту', NULL);
INSERT INTO `profession` VALUES ('17', 'Оператор call-центра', NULL);
INSERT INTO `profession` VALUES ('18', 'Оператор ПК', NULL);
INSERT INTO `profession` VALUES ('19', 'Офис-менеджер', NULL);
INSERT INTO `profession` VALUES ('20', 'Программист', NULL);
INSERT INTO `profession` VALUES ('21', 'Проектный менеджер', NULL);
INSERT INTO `profession` VALUES ('22', 'Разработчик C#', NULL);
INSERT INTO `profession` VALUES ('23', 'Разработчик Java', NULL);
INSERT INTO `profession` VALUES ('24', 'Разработчик Python', NULL);
INSERT INTO `profession` VALUES ('25', 'Сетевой администратор', NULL);
INSERT INTO `profession` VALUES ('26', 'Системный администратор', NULL);
INSERT INTO `profession` VALUES ('27', 'Специалист по PR', NULL);
INSERT INTO `profession` VALUES ('28', 'Специалист по интернет-маркетингу', NULL);
INSERT INTO `profession` VALUES ('29', 'Специалист по кадрам', NULL);
INSERT INTO `profession` VALUES ('30', 'Специалист по логистике', NULL);
INSERT INTO `profession` VALUES ('31', 'Специалист по обучению', NULL);
INSERT INTO `profession` VALUES ('32', 'Специалист по продажам', NULL);
INSERT INTO `profession` VALUES ('33', 'Специалист по финансам', NULL);
INSERT INTO `profession` VALUES ('34', 'Тестировщик', NULL);
INSERT INTO `profession` VALUES ('35', 'Финансовый аналитик', NULL);
INSERT INTO `profession` VALUES ('36', 'JavaScript разработчик', NULL);
INSERT INTO `profession` VALUES ('37', 'PHP разработчик', NULL);
INSERT INTO `profession` VALUES ('38', 'SEO специалист', NULL);
INSERT INTO `profession` VALUES ('39', 'Арт-директор', NULL);
INSERT INTO `profession` VALUES ('40', 'Бухгалтер', NULL);
INSERT INTO `profession` VALUES ('41', 'Ведущий разработчик', NULL);
INSERT INTO `profession` VALUES ('42', 'Интернет-маркетолог', NULL);
INSERT INTO `profession` VALUES ('43', 'Контент-менеджер', NULL);
INSERT INTO `profession` VALUES ('44', 'Разработчик игр', NULL);
INSERT INTO `profession` VALUES ('45', 'Разработчик мобильных приложений', NULL);
INSERT INTO `profession` VALUES ('46', 'Системный аналитик', NULL);
INSERT INTO `profession` VALUES ('47', 'Специалист по IT-безопасности', NULL);
INSERT INTO `profession` VALUES ('48', 'Специалист по качеству', NULL);
INSERT INTO `profession` VALUES ('49', 'Специалист по клиентскому сервису', NULL);
INSERT INTO `profession` VALUES ('50', 'Специалист по маркетингу', NULL);
INSERT INTO `profession` VALUES ('51', 'Специалист по недвижимости', NULL);
INSERT INTO `profession` VALUES ('52', 'Специалист по поддержке', NULL);
INSERT INTO `profession` VALUES ('53', 'Специалист по продукту', NULL);
INSERT INTO `profession` VALUES ('54', 'Специалист по рекламе', NULL);
INSERT INTO `profession` VALUES ('55', 'Старший менеджер по продажам', NULL);

DROP TABLE IF EXISTS resume;
CREATE TABLE `resume` (
  `id` int NOT NULL AUTO_INCREMENT,
  `resume_applicant` int NOT NULL,
  `resume_profession` int NOT NULL,
  `salary` decimal(7,0) NOT NULL,
  `resume_education` varchar(1000) NOT NULL,
  `resume_work_experience` varchar(1000) NOT NULL,
  `resume_knowledge_of_languages` varchar(150) NOT NULL,
  `resume_personal_qualities` varchar(100) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_resume_applicant1_idx` (`resume_applicant`),
  KEY `resume_profession_idx` (`resume_profession`),
  CONSTRAINT `fk_resume_applicant1` FOREIGN KEY (`resume_applicant`) REFERENCES `applicant` (`applicant_id`),
  CONSTRAINT `resume_profession` FOREIGN KEY (`resume_profession`) REFERENCES `profession` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=58 DEFAULT CHARSET=utf8mb3;

-- Data for table resume

INSERT INTO `resume` VALUES ('2', '2', '21', '95', ' Университет биологии, биолог, 2010-2015', 'АО \"Биотехнологии\", 5 лет: ООО \"Сетевые технологии\", 4 года: ЗАО \"Финансовый анализ\", 3 года', 'Русский, Немецкий', 'Лидерские качества, Умение работать в команде');
INSERT INTO `resume` VALUES ('3', '3', '21', '95', ' Университет биологии, биолог, 2010-2015', 'АО \"Биотехнологии\", 5 лет: ООО \"Сетевые технологии\", 4 года: ЗАО \"Финансовый анализ\", 3 года', 'Русский, Немецкий', 'Лидерские качества, Умение работать в команде');
INSERT INTO `resume` VALUES ('4', '4', '21', '9500000', ' Университет биологии, биолог, 2010-2015', 'АО \"Биотехнологии\", 5 лет: ООО \"Сетевые технологии\", 4 года: ЗАО \"Финансовый анализ\", 3 года', 'Русский, Немецкий', 'Лидерские качества, Умение работать в команде');
INSERT INTO `resume` VALUES ('5', '5', '21', '95', ' Университет биологии, биолог, 2010-2015', 'АО \"Биотехнологии\", 5 лет: ООО \"Сетевые технологии\", 4 года: ЗАО \"Финансовый анализ\", 3 года', 'Русский, Немецкий', 'Лидерские качества, Умение работать в команде');
INSERT INTO `resume` VALUES ('6', '6', '21', '95', ' Университет биологии, биолог, 2010-2015', 'АО \"Биотехнологии\", 5 лет: ООО \"Сетевые технологии\", 4 года: ЗАО \"Финансовый анализ\", 3 года', 'Русский, Немецкий', 'Лидерские качества, Умение работать в команде');
INSERT INTO `resume` VALUES ('7', '7', '20', '95', ' Университет биологии, биолог, 2010-2015', 'АО \"Биотехнологии\", 5 лет: ООО \"Сетевые технологии\", 4 года: ЗАО \"Финансовый анализ\", 3 года', 'Русский, Немецкий', 'Лидерские качества, Умение работать в команде');
INSERT INTO `resume` VALUES ('8', '8', '21', '95', ' Университет биологии, биолог, 2010-2015', 'АО \"Биотехнологии\", 5 лет: ООО \"Сетевые технологии\", 4 года: ЗАО \"Финансовый анализ\", 3 года', 'Русский, Немецкий', 'Лидерские качества, Умение работать в команде');
INSERT INTO `resume` VALUES ('9', '9', '21', '95', ' Университет биологии, биолог, 2010-2015', 'АО \"Биотехнологии\", 5 лет: ООО \"Сетевые технологии\", 4 года: ЗАО \"Финансовый анализ\", 3 года', 'Русский, Немецкий', 'Лидерские качества, Умение работать в команде');
INSERT INTO `resume` VALUES ('10', '10', '21', '95', ' Университет биологии, биолог, 2010-2015', 'АО \"Биотехнологии\", 5 лет: ООО \"Сетевые технологии\", 4 года: ЗАО \"Финансовый анализ\", 3 года', 'Русский, Немецкий', 'Лидерские качества, Умение работать в команде');
INSERT INTO `resume` VALUES ('11', '11', '21', '95', ' Университет биологии, биолог, 2010-2015', 'АО \"Биотехнологии\", 5 лет: ООО \"Сетевые технологии\", 4 года: ЗАО \"Финансовый анализ\", 3 года', 'Русский, Немецкий', 'Лидерские качества, Умение работать в команде');
INSERT INTO `resume` VALUES ('12', '12', '21', '95', ' Университет биологии, биолог, 2010-2015', 'АО \"Биотехнологии\", 5 лет: ООО \"Сетевые технологии\", 4 года: ЗАО \"Финансовый анализ\", 3 года', 'Русский, Немецкий', 'Лидерские качества, Умение работать в команде');
INSERT INTO `resume` VALUES ('13', '13', '21', '95', ' Университет биологии, биолог, 2010-2015', 'АО \"Биотехнологии\", 5 лет: ООО \"Сетевые технологии\", 4 года: ЗАО \"Финансовый анализ\", 3 года', 'Русский, Немецкий', 'Лидерские качества, Умение работать в команде');
INSERT INTO `resume` VALUES ('14', '14', '21', '95', ' Университет биологии, биолог, 2010-2015', 'АО \"Биотехнологии\", 5 лет: ООО \"Сетевые технологии\", 4 года: ЗАО \"Финансовый анализ\", 3 года', 'Русский, Немецкий', 'Лидерские качества, Умение работать в команде');
INSERT INTO `resume` VALUES ('15', '15', '21', '95', ' Университет биологии, биолог, 2010-2015', 'АО \"Биотехнологии\", 5 лет: ООО \"Сетевые технологии\", 4 года: ЗАО \"Финансовый анализ\", 3 года', 'Русский, Немецкий', 'Лидерские качества, Умение работать в команде');
INSERT INTO `resume` VALUES ('16', '16', '21', '95', ' Университет биологии, биолог, 2010-2015', 'АО \"Биотехнологии\", 5 лет: ООО \"Сетевые технологии\", 4 года: ЗАО \"Финансовый анализ\", 3 года', 'Русский, Немецкий', 'Лидерские качества, Умение работать в команде');
INSERT INTO `resume` VALUES ('17', '17', '21', '95', ' Университет биологии, биолог, 2010-2015', 'АО \"Биотехнологии\", 5 лет: ООО \"Сетевые технологии\", 4 года: ЗАО \"Финансовый анализ\", 3 года', 'Русский, Немецкий', 'Лидерские качества, Умение работать в команде');
INSERT INTO `resume` VALUES ('18', '18', '21', '95', ' Университет биологии, биолог, 2010-2015', 'АО \"Биотехнологии\", 5 лет: ООО \"Сетевые технологии\", 4 года: ЗАО \"Финансовый анализ\", 3 года', 'Русский, Немецкий', 'Лидерские качества, Умение работать в команде');
INSERT INTO `resume` VALUES ('19', '19', '21', '95', ' Университет биологии, биолог, 2010-2015', 'АО \"Биотехнологии\", 5 лет: ООО \"Сетевые технологии\", 4 года: ЗАО \"Финансовый анализ\", 3 года', 'Русский, Немецкий', 'Лидерские качества, Умение работать в команде');
INSERT INTO `resume` VALUES ('20', '20', '21', '95', ' Университет биологии, биолог, 2010-2015', 'АО \"Биотехнологии\", 5 лет: ООО \"Сетевые технологии\", 4 года: ЗАО \"Финансовый анализ\", 3 года', 'Русский, Немецкий', 'Лидерские качества, Умение работать в команде');
INSERT INTO `resume` VALUES ('21', '21', '21', '95', ' Университет биологии, биолог, 2010-2015', 'АО \"Биотехнологии\", 5 лет: ООО \"Сетевые технологии\", 4 года: ЗАО \"Финансовый анализ\", 3 года', 'Русский, Немецкий', 'Лидерские качества, Умение работать в команде');
INSERT INTO `resume` VALUES ('22', '22', '21', '95', ' Университет биологии, биолог, 2010-2015', 'АО \"Биотехнологии\", 5 лет: ООО \"Сетевые технологии\", 4 года: ЗАО \"Финансовый анализ\", 3 года', 'Русский, Немецкий', 'Лидерские качества, Умение работать в команде');
INSERT INTO `resume` VALUES ('23', '23', '21', '95', ' Университет биологии, биолог, 2010-2015', 'АО \"Биотехнологии\", 5 лет: ООО \"Сетевые технологии\", 4 года: ЗАО \"Финансовый анализ\", 3 года', 'Русский, Немецкий', 'Лидерские качества, Умение работать в команде');
INSERT INTO `resume` VALUES ('24', '24', '21', '95', ' Университет биологии, биолог, 2010-2015', 'АО \"Биотехнологии\", 5 лет: ООО \"Сетевые технологии\", 4 года: ЗАО \"Финансовый анализ\", 3 года', 'Русский, Немецкий', 'Лидерские качества, Умение работать в команде');
INSERT INTO `resume` VALUES ('25', '25', '21', '95', ' Университет биологии, биолог, 2010-2015', 'АО \"Биотехнологии\", 5 лет: ООО \"Сетевые технологии\", 4 года: ЗАО \"Финансовый анализ\", 3 года', 'Русский, Немецкий', 'Лидерские качества, Умение работать в команде');
INSERT INTO `resume` VALUES ('26', '26', '21', '95', ' Университет биологии, биолог, 2010-2015', 'АО \"Биотехнологии\", 5 лет: ООО \"Сетевые технологии\", 4 года: ЗАО \"Финансовый анализ\", 3 года', 'Русский, Немецкий', 'Лидерские качества, Умение работать в команде');
INSERT INTO `resume` VALUES ('27', '27', '21', '95', ' Университет биологии, биолог, 2010-2015', 'АО \"Биотехнологии\", 5 лет: ООО \"Сетевые технологии\", 4 года: ЗАО \"Финансовый анализ\", 3 года', 'Русский, Немецкий', 'Лидерские качества, Умение работать в команде');
INSERT INTO `resume` VALUES ('28', '28', '21', '95', ' Университет биологии, биолог, 2010-2015', 'АО \"Биотехнологии\", 5 лет: ООО \"Сетевые технологии\", 4 года: ЗАО \"Финансовый анализ\", 3 года', 'Русский, Немецкий', 'Лидерские качества, Умение работать в команде');
INSERT INTO `resume` VALUES ('29', '29', '21', '95', ' Университет биологии, биолог, 2010-2015', 'АО \"Биотехнологии\", 5 лет: ООО \"Сетевые технологии\", 4 года: ЗАО \"Финансовый анализ\", 3 года', 'Русский, Немецкий', 'Лидерские качества, Умение работать в команде');
INSERT INTO `resume` VALUES ('30', '30', '21', '95', ' Университет биологии, биолог, 2010-2015', 'АО \"Биотехнологии\", 5 лет: ООО \"Сетевые технологии\", 4 года: ЗАО \"Финансовый анализ\", 3 года', 'Русский, Немецкий', 'Лидерские качества, Умение работать в команде');
INSERT INTO `resume` VALUES ('31', '31', '21', '95', ' Университет биологии, биолог, 2010-2015', 'АО \"Биотехнологии\", 5 лет: ООО \"Сетевые технологии\", 4 года: ЗАО \"Финансовый анализ\", 3 года', 'Русский, Немецкий', 'Лидерские качества, Умение работать в команде');
INSERT INTO `resume` VALUES ('32', '32', '21', '95', ' Университет биологии, биолог, 2010-2015', 'АО \"Биотехнологии\", 5 лет: ООО \"Сетевые технологии\", 4 года: ЗАО \"Финансовый анализ\", 3 года', 'Русский, Немецкий', 'Лидерские качества, Умение работать в команде');
INSERT INTO `resume` VALUES ('33', '33', '21', '95', ' Университет биологии, биолог, 2010-2015', 'АО \"Биотехнологии\", 5 лет: ООО \"Сетевые технологии\", 4 года: ЗАО \"Финансовый анализ\", 3 года', 'Русский, Немецкий', 'Лидерские качества, Умение работать в команде');
INSERT INTO `resume` VALUES ('34', '34', '21', '95', ' Университет биологии, биолог, 2010-2015', 'АО \"Биотехнологии\", 5 лет: ООО \"Сетевые технологии\", 4 года: ЗАО \"Финансовый анализ\", 3 года', 'Русский, Немецкий', 'Лидерские качества, Умение работать в команде');
INSERT INTO `resume` VALUES ('35', '35', '21', '95', ' Университет биологии, биолог, 2010-2015', 'АО \"Биотехнологии\", 5 лет: ООО \"Сетевые технологии\", 4 года: ЗАО \"Финансовый анализ\", 3 года', 'Русский, Немецкий', 'Лидерские качества, Умение работать в команде');
INSERT INTO `resume` VALUES ('36', '36', '21', '95', ' Университет биологии, биолог, 2010-2015', 'АО \"Биотехнологии\", 5 лет: ООО \"Сетевые технологии\", 4 года: ЗАО \"Финансовый анализ\", 3 года', 'Русский, Немецкий', 'Лидерские качества, Умение работать в команде');
INSERT INTO `resume` VALUES ('37', '37', '21', '95', ' Университет биологии, биолог, 2010-2015', 'АО \"Биотехнологии\", 5 лет: ООО \"Сетевые технологии\", 4 года: ЗАО \"Финансовый анализ\", 3 года', 'Русский, Немецкий', 'Лидерские качества, Умение работать в команде');
INSERT INTO `resume` VALUES ('38', '38', '21', '95', ' Университет биологии, биолог, 2010-2015', 'АО \"Биотехнологии\", 5 лет: ООО \"Сетевые технологии\", 4 года: ЗАО \"Финансовый анализ\", 3 года', 'Русский, Немецкий', 'Лидерские качества, Умение работать в команде');
INSERT INTO `resume` VALUES ('39', '39', '21', '95', ' Университет биологии, биолог, 2010-2015', 'АО \"Биотехнологии\", 5 лет: ООО \"Сетевые технологии\", 4 года: ЗАО \"Финансовый анализ\", 3 года', 'Русский, Немецкий', 'Лидерские качества, Умение работать в команде');
INSERT INTO `resume` VALUES ('40', '40', '21', '95', ' Университет биологии, биолог, 2010-2015', 'АО \"Биотехнологии\", 5 лет: ООО \"Сетевые технологии\", 4 года: ЗАО \"Финансовый анализ\", 3 года', 'Русский, Немецкий', 'Лидерские качества, Умение работать в команде');
INSERT INTO `resume` VALUES ('41', '41', '21', '95', ' Университет биологии, биолог, 2010-2015', 'АО \"Биотехнологии\", 5 лет: ООО \"Сетевые технологии\", 4 года: ЗАО \"Финансовый анализ\", 3 года', 'Русский, Немецкий', 'Лидерские качества, Умение работать в команде');
INSERT INTO `resume` VALUES ('42', '42', '21', '95', ' Университет биологии, биолог, 2010-2015', 'АО \"Биотехнологии\", 5 лет: ООО \"Сетевые технологии\", 4 года: ЗАО \"Финансовый анализ\", 3 года', 'Русский, Немецкий', 'Лидерские качества, Умение работать в команде');
INSERT INTO `resume` VALUES ('43', '43', '21', '95', ' Университет биологии, биолог, 2010-2015', 'АО \"Биотехнологии\", 5 лет: ООО \"Сетевые технологии\", 4 года: ЗАО \"Финансовый анализ\", 3 года', 'Русский, Немецкий', 'Лидерские качества, Умение работать в команде');
INSERT INTO `resume` VALUES ('44', '44', '21', '95', ' Университет биологии, биолог, 2010-2015', 'АО \"Биотехнологии\", 5 лет: ООО \"Сетевые технологии\", 4 года: ЗАО \"Финансовый анализ\", 3 года', 'Русский, Немецкий', 'Лидерские качества, Умение работать в команде');
INSERT INTO `resume` VALUES ('45', '45', '21', '95', ' Университет биологии, биолог, 2010-2015', 'АО \"Биотехнологии\", 5 лет: ООО \"Сетевые технологии\", 4 года: ЗАО \"Финансовый анализ\", 3 года', 'Русский, Немецкий', 'Лидерские качества, Умение работать в команде');
INSERT INTO `resume` VALUES ('46', '46', '21', '95', ' Университет биологии, биолог, 2010-2015', 'АО \"Биотехнологии\", 5 лет: ООО \"Сетевые технологии\", 4 года: ЗАО \"Финансовый анализ\", 3 года', 'Русский, Немецкий', 'Лидерские качества, Умение работать в команде');
INSERT INTO `resume` VALUES ('47', '47', '21', '95', ' Университет биологии, биолог, 2010-2015', 'АО \"Биотехнологии\", 5 лет: ООО \"Сетевые технологии\", 4 года: ЗАО \"Финансовый анализ\", 3 года', 'Русский, Немецкий', 'Лидерские качества, Умение работать в команде');
INSERT INTO `resume` VALUES ('48', '48', '21', '95', ' Университет биологии, биолог, 2010-2015', 'АО \"Биотехнологии\", 5 лет: ООО \"Сетевые технологии\", 4 года: ЗАО \"Финансовый анализ\", 3 года', 'Русский, Немецкий', 'Лидерские качества, Умение работать в команде');
INSERT INTO `resume` VALUES ('49', '49', '21', '95', ' Университет биологии, биолог, 2010-2015', 'АО \"Биотехнологии\", 5 лет: ООО \"Сетевые технологии\", 4 года: ЗАО \"Финансовый анализ\", 3 года', 'Русский, Немецкий', 'Лидерские качества, Умение работать в команде');
INSERT INTO `resume` VALUES ('50', '50', '21', '95', ' Университет биологии, биолог, 2010-2015', 'АО \"Биотехнологии\", 5 лет: ООО \"Сетевые технологии\", 4 года: ЗАО \"Финансовый анализ\", 3 года', 'Русский, Немецкий', 'Лидерские качества, Умение работать в команде');
INSERT INTO `resume` VALUES ('51', '53', '21', '95', ' Университет биологии, биолог, 2010-2015', 'АО \"Биотехнологии\", 5 лет: ООО \"Сетевые технологии\", 4 года: ЗАО \"Финансовый анализ\", 3 года', 'Русский, Немецкий', 'Лидерские качества, Умение работать в команде');
INSERT INTO `resume` VALUES ('52', '52', '21', '95', ' Университет биологии, биолог, 2010-2015', 'АО \"Биотехнологии\", 5 лет: ООО \"Сетевые технологии\", 4 года: ЗАО \"Финансовый анализ\", 3 года', 'Русский, Немецкий', 'Лидерские качества, Умение работать в команде');
INSERT INTO `resume` VALUES ('54', '55', '21', '95', ' Университет биологии, биолог, 2010-2015', 'АО \"Биотехнологии\", 5 лет: ООО \"Сетевые технологии\", 4 года: ЗАО \"Финансовый анализ\", 3 года', 'Русский, Немецкий', 'Лидерские качества, Умение работать в команде');
INSERT INTO `resume` VALUES ('55', '1', '21', '95', ' Университет биологии, биолог, 2010-2015', 'АО \"Биотехнологии\", 5 лет: ООО \"Сетевые технологии\", 4 года: ЗАО \"Финансовый анализ\", 3 года', 'Русский, Немецкий', 'Лидерские качества, Умение работать в команде');
INSERT INTO `resume` VALUES ('56', '54', '21', '95', ' Университет биологии, биолог, 2010-2015', 'АО \"Биотехнологии\", 5 лет: ООО \"Сетевые технологии\", 4 года: ЗАО \"Финансовый анализ\", 3 года', 'Русский, Немецкий', 'Лидерские качества, Умение работать в команде');
INSERT INTO `resume` VALUES ('57', '65', '21', '95', ' Университет биологии, биолог, 2010-2015', 'АО \"Биотехнологии\", 5 лет: ООО \"Сетевые технологии\", 4 года: ЗАО \"Финансовый анализ\", 3 года', 'Русский, Немецкий', 'Лидерские качества, Умение работать в команде');

DROP TABLE IF EXISTS vacancy;
CREATE TABLE `vacancy` (
  `id` int NOT NULL AUTO_INCREMENT,
  `vacancy_company` int NOT NULL,
  `vacancy_profession` int NOT NULL,
  `vacancy_responsibilities` varchar(500) NOT NULL,
  `vacancy_requirements` varchar(500) NOT NULL,
  `vacancy_conditions` varchar(500) NOT NULL,
  `vacancy_address` varchar(200) NOT NULL,
  `vacancy_salary_by` decimal(7,0) NOT NULL,
  `vacancy_salary_before` decimal(7,0) NOT NULL,
  `vacancy_delete_status` int DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_vacancy_company1_idx` (`vacancy_company`),
  KEY `vacancy_profession_idx` (`vacancy_profession`),
  KEY `vacancy_delete_status_idx` (`vacancy_delete_status`),
  CONSTRAINT `fk_vacancy_company1` FOREIGN KEY (`vacancy_company`) REFERENCES `company` (`id`),
  CONSTRAINT `vacancy_delete_status` FOREIGN KEY (`vacancy_delete_status`) REFERENCES `delete` (`id`),
  CONSTRAINT `vacancy_profession` FOREIGN KEY (`vacancy_profession`) REFERENCES `profession` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=59 DEFAULT CHARSET=utf8mb3;

-- Data for table vacancy

INSERT INTO `vacancy` VALUES ('1', '1', '20', 'Разработка программного обеспечения', 'Знание Python', 'Гибкий график', 'г. Москва, ул. Программная, д. 1', '120000', '60000', NULL);
INSERT INTO `vacancy` VALUES ('2', '2', '20', 'Разработка программного обеспечения', 'Знание Python', 'Гибкий график', 'г. Москва, ул. Программная, д. 1', '120000', '60000', '1');
INSERT INTO `vacancy` VALUES ('3', '3', '20', 'Разработка программного обеспечения', 'Знание Python', 'Гибкий график', 'г. Москва, ул. Программная, д. 1', '120000', '60000', NULL);
INSERT INTO `vacancy` VALUES ('4', '4', '20', 'Разработка программного обеспечения', 'Знание Python', 'Гибкий график', 'г. Москва, ул. Программная, д. 1', '120000', '60000', '4');
INSERT INTO `vacancy` VALUES ('5', '5', '20', 'Разработка программного обеспечения', 'Знание Python', 'Гибкий график', 'г. Москва, ул. Программная, д. 1', '120000', '60000', '2');
INSERT INTO `vacancy` VALUES ('6', '6', '20', 'Разработка программного обеспечения', 'Знание Python', 'Гибкий график', 'г. Москва, ул. Программная, д. 1', '120000', '60000', '2');
INSERT INTO `vacancy` VALUES ('7', '7', '20', 'Разработка программного обеспечения', 'Знание Python', 'Гибкий график', 'г. Москва, ул. Программная, д. 1', '120000', '60000', '1');
INSERT INTO `vacancy` VALUES ('8', '8', '20', 'Разработка программного обеспечения', 'Знание Python', 'Гибкий график', 'г. Москва, ул. Программная, д. 1', '120000', '60000', NULL);
INSERT INTO `vacancy` VALUES ('9', '9', '20', 'Разработка программного обеспечения', 'Знание Python', 'Гибкий график', 'г. Москва, ул. Программная, д. 1', '120000', '60000', '2');
INSERT INTO `vacancy` VALUES ('10', '10', '20', 'Разработка программного обеспечения', 'Знание Python', 'Гибкий график', 'г. Москва, ул. Программная, д. 1', '120000', '60000', NULL);
INSERT INTO `vacancy` VALUES ('11', '11', '20', 'Разработка программного обеспечения', 'Знание Python', 'Гибкий график', 'г. Москва, ул. Программная, д. 1', '120000', '60000', NULL);
INSERT INTO `vacancy` VALUES ('12', '12', '20', 'Разработка программного обеспечения', 'Знание Python', 'Гибкий график', 'г. Москва, ул. Программная, д. 1', '120000', '60000', NULL);
INSERT INTO `vacancy` VALUES ('13', '13', '20', 'Знание Python', 'Разработка программного обеспечения', 'Гибкий график', 'г. Москва, ул. Программная, д. 1', '20000', '60000', NULL);
INSERT INTO `vacancy` VALUES ('14', '14', '20', 'Разработка программного обеспечения', 'Знание Python', 'Гибкий график', 'г. Москва, ул. Программная, д. 1', '120000', '60000', NULL);
INSERT INTO `vacancy` VALUES ('15', '15', '20', 'Разработка программного обеспечения', 'Знание Python', 'Гибкий график', 'г. Москва, ул. Программная, д. 1', '120000', '60000', NULL);
INSERT INTO `vacancy` VALUES ('16', '16', '20', 'Разработка программного обеспечения', 'Знание Python', 'Гибкий график', 'г. Москва, ул. Программная, д. 1', '120000', '60000', NULL);
INSERT INTO `vacancy` VALUES ('17', '17', '20', 'Разработка программного обеспечения', 'Знание Python', 'Гибкий график', 'г. Москва, ул. Программная, д. 1', '120000', '60000', NULL);
INSERT INTO `vacancy` VALUES ('18', '18', '20', 'Разработка программного обеспечения', 'Знание Python', 'Гибкий график', 'г. Москва, ул. Программная, д. 1', '120000', '60000', NULL);
INSERT INTO `vacancy` VALUES ('19', '19', '20', 'Разработка программного обеспечения', 'Знание Python', 'Гибкий график', 'г. Москва, ул. Программная, д. 1', '120000', '60000', NULL);
INSERT INTO `vacancy` VALUES ('20', '20', '20', 'Разработка программного обеспечения', 'Знание Python', 'Гибкий график', 'г. Москва, ул. Программная, д. 1', '120000', '60000', NULL);
INSERT INTO `vacancy` VALUES ('21', '21', '20', 'Разработка программного обеспечения', 'Знание Python', 'Гибкий график', 'г. Москва, ул. Программная, д. 1', '120000', '60000', NULL);
INSERT INTO `vacancy` VALUES ('22', '22', '20', 'Разработка программного обеспечения', 'Знание Python', 'Гибкий график', 'г. Москва, ул. Программная, д. 1', '120000', '60000', NULL);
INSERT INTO `vacancy` VALUES ('23', '23', '20', 'Разработка программного обеспечения', 'Знание Python', 'Гибкий график', 'г. Москва, ул. Программная, д. 1', '120000', '60000', NULL);
INSERT INTO `vacancy` VALUES ('24', '24', '20', 'Разработка программного обеспечения', 'Знание Python', 'Гибкий график', 'г. Москва, ул. Программная, д. 1', '120000', '60000', NULL);
INSERT INTO `vacancy` VALUES ('25', '25', '20', 'Разработка программного обеспечения', 'Знание Python', 'Гибкий график', 'г. Москва, ул. Программная, д. 1', '120000', '60000', NULL);
INSERT INTO `vacancy` VALUES ('26', '26', '20', 'Разработка программного обеспечения', 'Знание Python', 'Гибкий график', 'г. Москва, ул. Программная, д. 1', '120000', '60000', NULL);
INSERT INTO `vacancy` VALUES ('27', '27', '20', 'Разработка программного обеспечения', 'Знание Python', 'Гибкий график', 'г. Москва, ул. Программная, д. 1', '120000', '60000', NULL);
INSERT INTO `vacancy` VALUES ('28', '28', '20', 'Разработка программного обеспечения', 'Знание Python', 'Гибкий график', 'г. Москва, ул. Программная, д. 1', '120000', '60000', NULL);
INSERT INTO `vacancy` VALUES ('29', '29', '20', 'Разработка программного обеспечения', 'Знание Python', 'Гибкий график', 'г. Москва, ул. Программная, д. 1', '120000', '60000', NULL);
INSERT INTO `vacancy` VALUES ('30', '30', '20', 'Разработка программного обеспечения', 'Знание Python', 'Гибкий график', 'г. Москва, ул. Программная, д. 1', '120000', '60000', NULL);
INSERT INTO `vacancy` VALUES ('31', '31', '20', 'Разработка программного обеспечения', 'Знание Python', 'Гибкий график', 'г. Москва, ул. Программная, д. 1', '120000', '60000', NULL);
INSERT INTO `vacancy` VALUES ('32', '32', '20', 'Разработка программного обеспечения', 'Знание Python', 'Гибкий график', 'г. Москва, ул. Программная, д. 1', '120000', '60000', NULL);
INSERT INTO `vacancy` VALUES ('33', '33', '20', 'Разработка программного обеспечения', 'Знание Python', 'Гибкий график', 'г. Москва, ул. Программная, д. 1', '120000', '60000', NULL);
INSERT INTO `vacancy` VALUES ('34', '34', '20', 'Разработка программного обеспечения', 'Знание Python', 'Гибкий график', 'г. Москва, ул. Программная, д. 1', '120000', '60000', NULL);
INSERT INTO `vacancy` VALUES ('35', '35', '20', 'Разработка программного обеспечения', 'Знание Python', 'Гибкий график', 'г. Москва, ул. Программная, д. 1', '120000', '60000', NULL);
INSERT INTO `vacancy` VALUES ('36', '36', '20', 'Разработка программного обеспечения', 'Знание Python', 'Гибкий график', 'г. Москва, ул. Программная, д. 1', '120000', '60000', NULL);
INSERT INTO `vacancy` VALUES ('37', '37', '20', 'Разработка программного обеспечения', 'Знание Python', 'Гибкий график', 'г. Москва, ул. Программная, д. 1', '120000', '60000', NULL);
INSERT INTO `vacancy` VALUES ('38', '38', '20', 'Разработка программного обеспечения', 'Знание Python', 'Гибкий график', 'г. Москва, ул. Программная, д. 1', '120000', '60000', NULL);
INSERT INTO `vacancy` VALUES ('39', '39', '20', 'Разработка программного обеспечения', 'Знание Python', 'Гибкий график', 'г. Москва, ул. Программная, д. 1', '120000', '60000', NULL);
INSERT INTO `vacancy` VALUES ('40', '40', '20', 'Разработка программного обеспечения', 'Знание Python', 'Гибкий график', 'г. Москва, ул. Программная, д. 1', '120000', '60000', NULL);
INSERT INTO `vacancy` VALUES ('41', '41', '20', 'Разработка программного обеспечения', 'Знание Python', 'Гибкий график', 'г. Москва, ул. Программная, д. 1', '120000', '60000', NULL);
INSERT INTO `vacancy` VALUES ('42', '42', '20', 'Разработка программного обеспечения', 'Знание Python', 'Гибкий график', 'г. Москва, ул. Программная, д. 1', '120000', '60000', NULL);
INSERT INTO `vacancy` VALUES ('43', '43', '20', 'Разработка программного обеспечения', 'Знание Python', 'Гибкий график', 'г. Москва, ул. Программная, д. 1', '120000', '60000', NULL);
INSERT INTO `vacancy` VALUES ('44', '44', '20', 'Разработка программного обеспечения', 'Знание Python', 'Гибкий график', 'г. Москва, ул. Программная, д. 1', '120000', '60000', NULL);
INSERT INTO `vacancy` VALUES ('45', '45', '20', 'Разработка программного обеспечения', 'Знание Python', 'Гибкий график', 'г. Москва, ул. Программная, д. 1', '120000', '60000', NULL);
INSERT INTO `vacancy` VALUES ('46', '46', '20', 'Разработка программного обеспечения', 'Знание Python', 'Гибкий график', 'г. Москва, ул. Программная, д. 1', '120000', '60000', NULL);
INSERT INTO `vacancy` VALUES ('47', '47', '20', 'Разработка программного обеспечения', 'Знание Python', 'Гибкий график', 'г. Москва, ул. Программная, д. 1', '120000', '60000', NULL);
INSERT INTO `vacancy` VALUES ('48', '48', '20', 'Разработка программного обеспечения', 'Знание Python', 'Гибкий график', 'г. Москва, ул. Программная, д. 1', '120000', '60000', NULL);
INSERT INTO `vacancy` VALUES ('49', '49', '20', 'Разработка программного обеспечения', 'Знание Python', 'Гибкий график', 'г. Москва, ул. Программная, д. 1', '120000', '60000', NULL);
INSERT INTO `vacancy` VALUES ('50', '50', '20', 'Разработка программного обеспечения', 'Знание Python', 'Гибкий график', 'г. Москва, ул. Программная, д. 1', '120000', '60000', NULL);
INSERT INTO `vacancy` VALUES ('51', '53', '25', 'ewqweqwe', 'eqweqwe', 'eqweqwe', 'eweqqwe', '3123', '3123123', '4');
INSERT INTO `vacancy` VALUES ('52', '53', '29', 'eqwe', 'qweqw', 'eqweq', 'weqeqwe', '313123', '231231', NULL);
INSERT INTO `vacancy` VALUES ('53', '3', '24', 'eqweq', 'eqewq', 'eqweqwe', 'eqweqew', '312', '3123', NULL);
INSERT INTO `vacancy` VALUES ('54', '54', '25', 'уйцу', 'уйцу', 'уйцу', 'уйцуйцу', '3123', '3123123', NULL);
INSERT INTO `vacancy` VALUES ('55', '50', '2', 'цуйцуйцу', 'уцуй', 'уйцуйцуцй', '3213', '3123', '3131', NULL);
INSERT INTO `vacancy` VALUES ('56', '3', '3', 'йцуйцу', 'уйцуйц', 'уйцуйцу', 'цуйцуй', '3123', '3123', NULL);
INSERT INTO `vacancy` VALUES ('57', '3', '2', 'уйцуйцу', 'уйцуйуц', 'уйцуйц', 'уйцуйцуй', '31', '31', NULL);
INSERT INTO `vacancy` VALUES ('58', '4', '2', 'йцуйцу', 'уйу', 'уйцу', 'уйцуй', '23', '31231', NULL);

