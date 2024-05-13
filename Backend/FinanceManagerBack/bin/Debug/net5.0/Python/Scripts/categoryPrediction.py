import pandas as pd
import numpy as np
from statsmodels.tsa.arima.model import ARIMA

def main(dataPath, forecastSpending):
	data = pd.read_csv(dataPath)
	data[['Amount', 'CreationTime']] = data['Amount;CreationTime'].str.split(';', expand=True)
	data.drop(columns=['Amount;CreationTime'], inplace=True)
	data['CreationTime'] = pd.to_datetime(data['CreationTime'], format='%d')
	data.set_index('CreationTime', inplace=True)
	data['Amount'] = pd.to_numeric(data['Amount'])
	data['Amount'] = data['Amount'].cumsum()

	train_size = int(len(data) * 0.8)
	train, test = data.iloc[:train_size], data.iloc[train_size:]

	model = ARIMA(train, order = (15, 1, 15))
	fit = model.fit()
	predict = fit.predict(test.index[0], test.index[-1])

	forecasted_values = fit.forecast(steps=31)

	message = "Spending will not reach " + str(forecastSpending) + " within the 1 month."

	for day, amount in enumerate(forecasted_values):
		if amount <= forecastSpending:
			message = "Day when spending will be " + str(forecastSpending) + " : " + str(day + 1)
			break

	return str(forecasted_values.values) + "\n" + message