#הופך מערך של dataframe למערך של numpyarray
def data_frame_to_numpy(dataframe, number_of_columns):
  numpy_array = dataframe.iloc[:, 0:number_of_columns].values
  return numpy_array

#הופך string ל int
def str_to_int(data):
  labelencoder_data = LabelEncoder()
  data[:, 5] = labelencoder_data.fit_transform(data[:, 5 ])
  data[:, 4] = labelencoder_data.fit_transform(data[:, 4 ])
  data[:, 6] = labelencoder_data.fit_transform(data[:, 6 ])
  return data

#הופך מ numpyarray ל dataframe
def array_to_dataframe(numpyarray, columns):
  df = pd.DataFrame(numpyarray)
  df.columns = columns #['grade', 'takers', 'studyunits', 'year', 'subject', 'city', 'school']
  df = df.astype('float64')
  return df

#מעביר את כל הערכים לערכים בין 0 ל1
def transform_to_0_1(numpy_array):
  scaler = MinMaxScaler()
  numpy_array[['subject','city', 'school', 'takers', 'year', 'grade']] = scaler.fit_transform(numpy_array[['subject','city', 'school', 'takers', 'year', 'grade']])
  return numpy_array

#בודק בכמה כל אחד מהנתונים משפיע על המשתנה שלנו
def conections_between_data(dataframe):
  sns.pairplot(dataframe,x_vars=['takers', 'studyunits', 'year', 'subject', 'city', 'school', 'economic'],y_vars= 'grade',kind="reg", size=6)
  dataframe.corr()['grade'][:].sort_values(ascending=False)
  return dataframe.corr()[['grade']].sort_values('grade',ascending=False)


#יוצר מערך עם העמודה grade שעליה אנחנו מריצים את המודל

def create_y(dataframe):
  y_t = np.array(dataframe['grade'])
  return y_t

#יוצר מערך עם כל העמודות חוץ מהעמודה grade
def create_x(dataframe):
  x_t = dataframe
  x_t = dataframe.drop(['grade'],axis=1)
  x_t = np.array(x_t)
  return x_t

#עושה פקטור לציון לפי כמות היחידות. ככל שהיחידה יותר גבוהה אז הוא מוסיף יותר נקודות לציון.
def calc_grade(df):
  i = 0
  for index, row in df.iterrows():
    df.at[i, 'grade'] =  row['grade'] + score(row['studyunits'])
    i = i + 1


def score(x):
    return {
        2: 0,
        3: 10,
        4: 20,
        5: 30
    }[x]