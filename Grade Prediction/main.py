import numpy as np
import pandas as pd 
import time
import seaborn as sns
import matplotlib.pyplot as plt
from matplotlib.colors import ListedColormap
from sklearn.utils import shuffle
from sklearn.svm import SVC
from sklearn.preprocessing import LabelEncoder
from sklearn.preprocessing import MinMaxScaler
from sklearn.model_selection import train_test_split
from sklearn.linear_model import LinearRegression
import sys
from sklearn.svm import SVR
from utils import calc_grade, data_frame_to_numpy, str_to_int, array_to_dataframe, transform_to_0_1, create_y, create_x, \
  conections_between_data

def main():
  #מכניס את המאגר נתונים הראשי שלנו לקובץ df ואת מאגר הנתונים עם המצב הכלכלי למשתנה economic
  df = pd.read_csv('/content/drive/MyDrive/data /israel_bagrut_averages-trim.csv')
  economic = pd.read_csv('/content/drive/MyDrive/data /city_economics.csv')
  economic.index = economic.iloc[:,1].values

  #מבצע פקטור על הציונים על פי הפעולה שהראיתי לפניי
  calc_grade(df)

  #מחבר את הקובץ economic עם הקובץ הראשי שלי על פי העמודה city ומוריד את העמודה city2 מהקובץ economic מפניי שאנחנו לא צריכים אותה
  df = df.join(economic, on='city')
  df.drop(['city2'],axis=1,inplace=True)


  #מדפיס את ה 5 שורות הראשונות
  df.head()


  #נותן לנו נתונים על כל המשתנים. הוא מאפשר לנו לראות האם יש נתונים חסרים או האם יש נתונים שהם objectim שצריך להמיר ל int.
  df.info()

  #מראה את כל המידע (חציון, ממוצע, מינימום, מקסימום, סטיית תקן וכדומה) על כל הערכים המספריים בבסיס הנתונים
  df.describe().transpose()
  #מוריד את העמודה semel מפני שהיא מייצגת את שמות בתי הספר רק במספר ולכן היא לא נחוצה
  df.drop(['semel'], axis=1, inplace=True)
  df.info

  #מוריד את כל השורות שחסר בהן ערכים
  df = df.dropna()
  df.info

  #מבלגן את כל הנתונים בצורה אקראית
  df = shuffle(df)

  #מעביר את הנתונים מdataframe ל numpyarray
  numpy_array = data_frame_to_numpy(df, 9)
  #הופך string ל int
  transform_to_int = str_to_int(numpy_array)

  #הופך מ numpyarray ל dataframe
  df1 = array_to_dataframe(transform_to_int, df.columns)

  #מעביר את כל הערכים לערכים בין 0 ל1
  transform_to_small = transform_to_0_1(df1)
  data = transform_to_small

  #משתמש בפעולה conections_between_data ומראה גם בצורה גרפית וגם בצורה מספרית כמה כל אחד מהנתונים משפיע על הציון
  conections_between_data(data)


  #לוקח את מאגר הנתונים המוכן לשימוש ומחלק אותו לשני מערכים. באחד יש רק את העמודה grade שעליה אנחנו מריצים את המודל ובשני יש את כל שאר הנתונים חוץ מהעמודה grade
  x_t = create_x(data)
  y_t = create_y(data)


  #מחלק את הנתונים לנתונים של train ו test

  x_train,x_test,y_train,y_test = train_test_split(x_t,y_t,test_size=.20,random_state=40)



  #מפעיל את המודל של LinearRegression על הנתוני train שלך ובודק כמה הוא מדויק על הנתוני test
  regressor = LinearRegression()
  regressor.fit(x_train ,y_train)

  accuracy = regressor.score(x_test,y_test)
  print(accuracy*100,'%')

  #אני עושה drop לשלוש עמודות שיש להן הכי פחות השפעה על ה grade ובודק האם זה משפר את הדיוק
  df_train = array_to_dataframe(x_train, [ 'takers', 'studyunits', 'year', 'subject', 'city', 'school', 'economic'])
  df_test = array_to_dataframe(x_test, [ 'takers', 'studyunits', 'year', 'subject', 'city', 'school', 'economic'])

  df_train.drop(['school','city', 'year' ],axis=1,inplace=True)
  df_test.drop(['school', 'city', 'year' ],axis=1,inplace=True)
  print(df_train)
  x1_train = data_frame_to_numpy(df_train, 4)
  x1_test = data_frame_to_numpy(df_test, 4)


  regressor = LinearRegression()
  regressor.fit(x1_train ,y_train)
  y_pred = regressor.predict(x1_test)
  accuracy = regressor.score(x1_test,y_test)
  print(accuracy*100,'%')
  for kernel in ['linear','poly','rbf']:
    for c in [0.1, 1, 10,100]:
      for gamma in ['scale', 'auto']:
        for degree in [1,2,3]:
          t0 = time.time()
          svm2 = SVR(kernel=kernel,C=c,gamma = gamma,degree = degree)
          svm2.fit(x_test, y_test)
          t1 = time.time()
          #print("time: {:.1f}".format(t1-t0))
          print("kernel: {0}  c: {1} gamma: {2} degree: {3} score: {4} time: {5}  ".format(kernel,c,gamma,degree,svm2.score(x_test,y_test),(t1-t0) ))




