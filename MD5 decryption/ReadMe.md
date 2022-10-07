<h2  dir='rtl'> MD5 decryption
<h3  dir='rtl'>  מטרת הפרויקט 
<h4  dir='rtl'>
 
 מטרת הפרויקט היא לגלות מהו המספר המקורי שהוצפן באמצעות פרוטוקול ההצפנה MD5
  
<h3  dir='rtl'>טכנולוגיות
<h4  dir='rtl'>
Python, client server application, multiprocessing, multithreading, hashlib encryption 

<h3  dir='rtl'> תכולת הפרויקט ושיטת עבודה

<h4  dir='rtl'>
הפרויקט בנוי מ server וכמה clients. ה server  מקבל את תוצאת ההרצה של פרוטוקול ההצפנה MD5 על מספר רנדומלי בעל 10 ספרות ומטרת ה server זה למצוא מספר זה באמצעות שימוש בכוח ההרצה של ה clients  העומדים לרשותו. בתחילת ההרצה ה server שולח לכל client  את המספר המוצפן שאותו הוא רוצה למצוא ולאחר מכן הוא שולח לכל client  קבוצה של מספרים   שעליהם  ה client  צריך לעבור. ה client  מצפין כל מספר ומשווה את ההצפנה שלו להצפנה שהוא קיבל מה server בהתחלה עד שהוא מוצא התאמה. ברגע שיש התאמה זה אומר שמצאנו את המספר שאנחנו צריכים וה client שולח אותו ל server ואז ה  server שולח הודעה לכל ה clients להפסיק לרוץ. בתוך ה clients  אני משתמש בשיטה של multithreading ו  multiprocessing בשביל להשתמש בצורה יעילה במשאבים של ה client ולזרז את הריצה. בגלל שהגרסת פייתון שאני עובד עליה לא תומכת ב multithreading אני משתמש ב multiprocessing ויוצר בכל client  כמה Process בשביל לנצל את  המשאבים של המחשב בצורה יותר יעילה