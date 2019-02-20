using System;

namespace odec.Server.Model.Store.Helpers.Enums
{
    /// <summary>
    /// Good Init flags
    /// </summary>
    [Flags]
    public enum GoodInitOptions
    {
        /// <summary>
        /// Init only sizes
        /// </summary>
        InitSize = 1,
        /// <summary>
        /// Init only colors
        /// </summary>
        InitColors = 2,
        /// <summary>
        /// Init main good image
        /// </summary>
        InitSingleImage = 4,
        /// <summary>
        /// Init good gallery
        /// </summary>
        InitImageGallery = 8,
        /// <summary>
        /// Init good Designers
        /// </summary>
        InitDesigners = 16,
        /// <summary>
        /// Init good Categories
        /// </summary>
        InitCategories = 32,
        /// <summary>
        /// Init Good types
        /// </summary>
        InitTypes = 64,
        /// <summary>
        /// Init good Materials
        /// </summary>
        InitMaterial = 128,
        /// <summary>
        /// Init colors and sizes
        /// </summary>
        InitColorsAndSizes = InitSize | InitColors,
        /// <summary>
        /// Init main image property and the gallery
        /// </summary>
        InitAllImages = InitSingleImage | InitImageGallery,
        /// <summary>
        /// Init Colors Sizes and designers
        /// </summary>
        InitColorsSizesDesigners = InitColorsAndSizes | InitDesigners,
        /// <summary>
        /// Default behavior: Init Colors, all images, designers, types and materials
        /// </summary>
        Default = InitColorsAndSizes | InitAllImages | InitDesigners | InitTypes | InitMaterial,
        /// <summary>
        /// Init all linked with good entities
        /// </summary>
        InitAll = InitColorsAndSizes | InitAllImages | InitDesigners | InitCategories | InitTypes | InitMaterial

    }



}
