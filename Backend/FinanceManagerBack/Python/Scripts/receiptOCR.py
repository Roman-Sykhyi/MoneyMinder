import pytesseract
from PIL import Image

def main(tesseractPath, imagePath):
	pytesseract.pytesseract.tesseract_cmd = tesseractPath

	text = pytesseract.image_to_string(Image.open(imagePath), lang="eng")

	total_spending = None
	lines = text.split('\n')
	for line in lines:
		if 'total' in line.lower():
			total_spending = line.split(':')[-1].strip()
			break

	return total_spending