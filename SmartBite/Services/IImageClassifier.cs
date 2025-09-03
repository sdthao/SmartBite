using SmartBite.Common;

namespace SmartBite.Services
{
    public interface IImageClassifier
    {
        /// <summary>
        /// Asynchronously processes the image at the specified path to retrieve a classified label.
        /// </summary>
        /// <param name="imagePath">The image path to classify.</param>
        /// <returns>Success/Fail process result. If successful, the predicted classified label.</returns>
        Task<IResult<string>> ClassifyImageAsync(string imagePath);

        /// <summary>
        /// Asynchronously processes the image to retrieve a classified label.
        /// </summary>
        /// <param name="imageData">The image to classify.</param>
        /// <returns>Success/Fail process result. If successful, the predicted classified label.</returns>
        Task<IResult<string>> ClassifyImageAsync(byte[] imageData);
    }
}
